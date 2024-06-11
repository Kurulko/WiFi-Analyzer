using Microsoft.EntityFrameworkCore;
using NativeWifi;
using WiFi_Analyzer.Database;
using WiFi_Analyzer.Extensions;
using WiFi_Analyzer.Models;
using static NativeWifi.Wlan;

namespace WiFi_Analyzer.Services.Networks;

public class NetworksService : NetworkService, INetworksService
{
    readonly WiFiAnalyzerContext db;
    public NetworksService(WiFiAnalyzerContext db)
        => this.db = db;

    DbSet<WiFiNetwork> dbSet => db.WiFiNetworks;

    public async Task<IEnumerable<WiFiNetwork>> GetWiFiNetworksWithStatesAsync()
    {
        IEnumerable<WiFiNetwork> networks = await GetWiFiNetworksAsync();

        foreach (WiFiNetwork network in networks)
            network.NetworkStates = GetNetworkStates(network);

        return networks;
    }

    public async Task<IEnumerable<WiFiNetwork>> GetWiFiNetworksAsync()
        => await dbSet.ToListAsync();

    public async Task AddWiFiNetworkAsync(WiFiNetwork network)
    {
        var existingNetwork = await GetWiFiNetworkByIdAsync(network.Id);
        if (existingNetwork is null)
        {
            await dbSet.AddAsync(network);
            await SaveChangesAsync();
        }
    }

    public async Task UpdateWiFiNetworkAsync(WiFiNetwork network)
    {
        WiFiNetwork? _network = await GetWiFiNetworkByBSSIDAsync(network.StringMacAddress)!;

        if (_network is not null)
        {
            CopyWiFiNetworkValues(network, _network);
            dbSet.Update(_network);
            await SaveChangesAsync();
        }
    }

    async Task DeleteWiFiNetworkAsync(WiFiNetwork? network)
    {
        if (network is not null)
        {
            dbSet.Remove(network);
            await SaveChangesAsync();
        }
    }

    public async Task DeleteWiFiNetworkByIdAsync(long networkId)
    {
        WiFiNetwork? network = await GetWiFiNetworkByIdAsync(networkId);
        await DeleteWiFiNetworkAsync(network);
    }

    public async Task DeleteWiFiNetworkByBSSIDAsync(string BSSID)
    {
        WiFiNetwork? network = await GetWiFiNetworkByBSSIDAsync(BSSID);
        await DeleteWiFiNetworkAsync(network);
    }

    public async Task<WiFiNetwork?> GetWiFiNetworkByIdAsync(long id)
        => (await GetWiFiNetworksAsync()).SingleOrDefault(n => n.Id == id);

    public async Task<WiFiNetwork?> GetWiFiNetworkByBSSIDAsync(string BSSID)
        => (await GetWiFiNetworksAsync()).SingleOrDefault(n => n.StringMacAddress == BSSID);

    public async Task UpdateWiFiNetworksAsync()
    {
        WlanClient client = new WlanClient();

        foreach (WlanClient.WlanInterface wlanInterface in client.Interfaces)
        {
            WlanBssEntry[] bssEntries = wlanInterface.GetNetworkBssList();

            foreach (var bssEntry in bssEntries)
            {
                string ssid = GetStringForSSID(bssEntry.dot11Ssid);
                long frequency = GetFrequencyFromChannel(bssEntry.chCenterFrequency);
                byte[] macAddress = bssEntry.dot11Bssid;
                int channel = GetChannelFromFrequency(frequency);
                string protocol = FindProtocolString(bssEntry);


                if (GetWlanAvailableNetworkByProfileName(ssid) is WlanAvailableNetwork wlanAvailableNetwork)
                {
                    Dot11AuthAlgorithm authenticationAlgorithm = wlanAvailableNetwork.dot11DefaultAuthAlgorithm;
                    bool isSecured = wlanAvailableNetwork.securityEnabled;

                    WiFiNetwork network = new()
                    {
                        SSID = ssid,
                        FrequencyInHz = frequency,
                        Channel = channel,
                        MacAddress = macAddress,
                        IsSecured = isSecured,
                        AuthenticationAlgorithm = authenticationAlgorithm,
                        Protocol = protocol,
                        LastSeen = DateTime.Now
                    };

                    string macAddressStr = macAddress.MacAddressToString();
                    WiFiNetwork? _network = await GetWiFiNetworkByBSSIDAsync(macAddressStr);

                    if (_network is null)
                    {
                        await AddWiFiNetworkAsync(network);
                    }
                    else
                    {
                        CopyWiFiNetworkValues(network, _network);
                        await UpdateWiFiNetworkAsync(_network);
                    }
                }
            }
        }
    }

    public NetworkStates? GetNetworkStates(WiFiNetwork network)
    {
        WlanClient client = new WlanClient();
        foreach (WlanClient.WlanInterface wlanInterface in client.Interfaces)
        {
            WlanBssEntry[] bssEntries = wlanInterface.GetNetworkBssList();

            foreach (var bssEntry in bssEntries)
            {
                string macAddressStr = bssEntry.dot11Bssid.MacAddressToString();

                if (network.StringMacAddress == macAddressStr)
                {
                    string ssid = GetStringForSSID(bssEntry.dot11Ssid);
                    int signalStrength = bssEntry.rssi;
                    long frequency = GetFrequencyFromChannel(bssEntry.chCenterFrequency);
                    double distance = CalculateDistance(signalStrength, frequency);

                    bool isConnected = false;

                    if (wlanInterface.InterfaceState == WlanInterfaceState.Connected)
                    {
                        string connectedSSID = GetStringForSSID(wlanInterface.CurrentConnection.wlanAssociationAttributes.dot11Ssid);

                        if (ssid == connectedSSID)
                            isConnected = true;
                    }

                    return new NetworkStates()
                    {
                        IsConnected = isConnected,
                        DistanceInMeters = distance,
                        SignalStrengthIndBm = signalStrength
                    };
                }
            }
        }

        return null;
    }

    void CopyWiFiNetworkValues(WiFiNetwork fromNetwork, WiFiNetwork toNetwork)
    {
        toNetwork.SSID = fromNetwork.SSID;
        toNetwork.FrequencyInHz = fromNetwork.FrequencyInHz;
        toNetwork.Channel = fromNetwork.Channel;
        toNetwork.IsSecured = fromNetwork.IsSecured;
        toNetwork.AuthenticationAlgorithm = fromNetwork.AuthenticationAlgorithm;
        toNetwork.Protocol = fromNetwork.Protocol;
        toNetwork.LastSeen = fromNetwork.LastSeen;
    }

    async Task SaveChangesAsync()
        => await db.SaveChangesAsync();
}
