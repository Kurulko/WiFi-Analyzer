using ManagedNativeWifi;
using NativeWifi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WiFi_Analyzer.Models;
using static NativeWifi.Wlan;

namespace WiFi_Analyzer.Services.Networks;

public class NetworksService : NetworkService, INetworksService
{
    public IEnumerable<WiFiNetwork> GetWiFiNetworks()
    {
        List<WiFiNetwork> networks = new List<WiFiNetwork>();
        WlanClient client = new WlanClient();
        foreach (WlanClient.WlanInterface wlanInterface in client.Interfaces)
        {
            WlanBssEntry[] bssEntries = wlanInterface.GetNetworkBssList();

            foreach (var bssEntry in bssEntries)
            {
                string ssid = GetStringForSSID(bssEntry.dot11Ssid);
                long frequency = GetFrequencyFromChannel(bssEntry.chCenterFrequency);
                int signalStrength = bssEntry.rssi;
                double distance = CalculateDistance(signalStrength, frequency);
                byte[] macAddress = bssEntry.dot11Bssid;
                int channel = GetChannelFromFrequency(frequency);


                bool isConnected = false;

                if (wlanInterface.InterfaceState == WlanInterfaceState.Connected)
                {
                    string bconnectedSSID = GetStringForSSID(wlanInterface.CurrentConnection.wlanAssociationAttributes.dot11Ssid);

                    if (ssid == bconnectedSSID)
                        isConnected = true;
                }


                if (GetWlanAvailableNetworkByProfileName(ssid) is WlanAvailableNetwork wlanAvailableNetwork)
                {
                    Dot11AuthAlgorithm authenticationAlgorithm = wlanAvailableNetwork.dot11DefaultAuthAlgorithm;
                    bool isSecured = wlanAvailableNetwork.securityEnabled;

                    networks.Add(new WiFiNetwork
                    {
                        SSID = ssid,
                        FrequencyInHz = frequency,
                        Channel = channel,
                        SignalStrengthIndBm = signalStrength,
                        MacAddress = macAddress,
                        DistanceInMeters = distance,
                        IsConnected = isConnected,
                        IsSecured = isSecured,
                        AuthenticationAlgorithm = authenticationAlgorithm
                    });
                }
            }
        }

        return networks;
    }
}
