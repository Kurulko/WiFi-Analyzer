using NativeWifi;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Net;
using WiFi_Analyzer.Models;
using static NativeWifi.Wlan;

namespace WiFi_Analyzer.Services.ConnectedNetwork;

public class ConnectedNetworkService : NetworkService, IConnectedNetworkService
{
    WlanBssEntry GetConnectedWlanBssEntry()
    {
        WlanClient client = new();

        foreach (WlanClient.WlanInterface wlanInterface in client.Interfaces)
        {
            if (wlanInterface.InterfaceState == WlanInterfaceState.Connected)
            {
                Dot11Ssid ssid = wlanInterface.CurrentConnection.wlanAssociationAttributes.dot11Ssid;
                WlanBssEntry[] bssEntries = wlanInterface.GetNetworkBssList();

                foreach (WlanBssEntry bssEntry in bssEntries)
                {
                    string currentSSID = GetStringForSSID(ssid);
                    string bssEntrySSID = GetStringForSSID(bssEntry.dot11Ssid);

                    if (currentSSID == bssEntrySSID)
                        return bssEntry;
                }
            }
        }

        throw new Exception("No internet connection detected.");
    }

    public NetworkStates GetConnectedNetworkStates()
    {
        WlanBssEntry connectedBssEntry = GetConnectedWlanBssEntry();

        long frequency = GetFrequencyFromChannel(connectedBssEntry.chCenterFrequency);
        int signalStrength = connectedBssEntry.rssi;

        NetworkStates networkStates = new();

        networkStates.DistanceInMeters = CalculateDistance(signalStrength, frequency);
        networkStates.IsConnected = true;
        networkStates.SignalStrengthIndBm = signalStrength;

        return networkStates;
    }

    public WiFiNetwork GetConnectedWiFiNetwork()
    {
        WlanBssEntry connectedBssEntry = GetConnectedWlanBssEntry();

        WiFiNetwork wiFiNetwork = new();

        long frequency = GetFrequencyFromChannel(connectedBssEntry.chCenterFrequency);
        string currentSSID = GetStringForSSID(connectedBssEntry.dot11Ssid);

        wiFiNetwork.SSID = currentSSID;
        wiFiNetwork.Channel = GetChannelFromFrequency(frequency);
        wiFiNetwork.FrequencyInHz = frequency;
        wiFiNetwork.Protocol = FindProtocolString(connectedBssEntry);
        wiFiNetwork.MacAddress = connectedBssEntry.dot11Bssid;

        WlanAvailableNetwork wlanAvailableNetwork = GetWlanAvailableNetworkByProfileName(currentSSID)!.Value;

        wiFiNetwork.IsSecured = wlanAvailableNetwork.securityEnabled;
        wiFiNetwork.AuthenticationAlgorithm = wlanAvailableNetwork.dot11DefaultAuthAlgorithm;

        return wiFiNetwork;
    }

    public async Task<IPAddressInfo> GetConnectedIPAddressInfo()
    {
        IPAddressInfo iPAddressInfo = new ();

        iPAddressInfo.PrivateIPv4 = GetPrivateIPv4();
        iPAddressInfo.PublicIPv4 = await GetPublicIPv4();
        iPAddressInfo.SubnetMask = GetSubnetMask();

        return iPAddressInfo;
    }

    string GetPrivateIPv4()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        var ipAddress = host.AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
        return ipAddress!.ToString();
    }

    string GetSubnetMask()
    {
        var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
        foreach (var networkInterface in networkInterfaces)
        {
            if (networkInterface.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 ||
                networkInterface.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
            {
                foreach (var unicastIPAddressInformation in networkInterface.GetIPProperties().UnicastAddresses)
                {
                    if (unicastIPAddressInformation.Address.AddressFamily == AddressFamily.InterNetwork)
                    {
                        return unicastIPAddressInformation.IPv4Mask!.ToString();
                    }
                }
            }
        }
        return null!;
    }

    async Task<string> GetPublicIPv4()
    {
        using (HttpClient httpClient = new())
            return (await httpClient.GetStringAsync("http://icanhazip.com")).Trim();
    }

    public NetworkSecurityInfo GetConnectedNetworkSecurityInfo()
    {
        NetworkSecurityInfo networkSecurityInfo = new();

        var client = new WlanClient();

        foreach (var wlanInterface in client.Interfaces)
        {
            var currentConnection = wlanInterface.CurrentConnection;

            WlanAvailableNetwork[] networks = wlanInterface.GetAvailableNetworkList(0);
            foreach (WlanAvailableNetwork network in networks)
            {
                if (network.profileName == currentConnection.profileName)
                {
                    networkSecurityInfo.Authentication = network.dot11DefaultAuthAlgorithm;
                    networkSecurityInfo.Encryption = network.dot11DefaultCipherAlgorithm;
                    break;
                }
            }
        }

        return networkSecurityInfo;
    }

    public NetworkInfrastructureInfo GetConnectedNetworkInfrastructureInfo()
    {
        NetworkInfrastructureInfo networkInfrastructureInfo = new();

        var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

        var currentInterface = networkInterfaces.FirstOrDefault(nic => nic.OperationalStatus == OperationalStatus.Up);

        if (currentInterface != null)
        {
            networkInfrastructureInfo.InterfaceType = currentInterface.NetworkInterfaceType;
            networkInfrastructureInfo.OperationalStatus = currentInterface.OperationalStatus;
            networkInfrastructureInfo.Interface = currentInterface.Description;
        }

        return networkInfrastructureInfo;
    }
}
