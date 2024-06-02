using NativeWifi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WiFi_Analyzer.Models;
using static NativeWifi.Wlan;

namespace WiFi_Analyzer.Services;

public abstract class NetworkService
{
    protected string GetStringForSSID(Dot11Ssid ssid)
        => Encoding.ASCII.GetString(ssid.SSID, 0, (int)ssid.SSIDLength);

    protected string FindProtocolString(WlanBssEntry bssEntry)
    {
        int rssi = bssEntry.rssi;
        uint chCenterFrequency = bssEntry.chCenterFrequency;

        if (chCenterFrequency >= 2400000 && chCenterFrequency < 2500000)
        {
            // 2.4 GHz band
            if (rssi <= -90)
                return "802.11b";
            else if (rssi <= -75)
                return "802.11g";
            else if (rssi <= -50)
                return "802.11n";
            else
                return "802.11ax"; // Likely 802.11ax due to strong signal in 2.4 GHz
        }
        else if (chCenterFrequency >= 5000000 && chCenterFrequency < 6000000)
        {
            // 5 GHz band
            if (rssi <= -70)
                return "802.11a";
            else if (rssi <= -50)
                return "802.11n";
            else
                return "802.11ac/ax"; // Likely newer standard (ac or ax) due to strong signal
        }
        else if (chCenterFrequency >= 57000000 && chCenterFrequency <= 66000000)
        {
            // Tentative check for 60 GHz band (needs verification)
            return "802.11ad";
        }
        else
        {
            // Frequency band not identified
            return "Unknown";
        }
    }

    protected WlanAvailableNetwork? GetWlanAvailableNetworkByProfileName(string profileName)
    {
        WlanClient wlanClient = new WlanClient();

        foreach (WlanClient.WlanInterface wlanInterface in wlanClient.Interfaces)
        {
            WlanAvailableNetwork[] availableNetworks = wlanInterface.GetAvailableNetworkList(0);
            foreach (WlanAvailableNetwork network in availableNetworks)
            {
                string networkProfileName = GetStringForSSID(network.dot11Ssid);
                if (networkProfileName == profileName)
                    return network;
            }
        }

        return null;
    }

    protected long GetFrequencyFromChannel(long channelFrequency)
        => (long)(channelFrequency * Math.Pow(10, 3)); //from kHz to Hz

    protected int GetChannelFromFrequency(long frequency)
    {
        int frequencyInMHz = FrequencyInMHz(frequency);

        if (frequencyInMHz >= 2412 && frequencyInMHz <= 2472)
        {
            return (frequencyInMHz - 2407) / 5;
        }
        else if (frequencyInMHz == 2484)
        {
            return 14;
        }
        else if (frequencyInMHz >= 5180 && frequencyInMHz <= 5825)
        {
            return (frequencyInMHz - 5000) / 5;
        }
        return -1; // Unknown channel
    }

    protected double CalculateDistance(int signalStrength, long frequency)
    {
        int frequencyInMHz = FrequencyInMHz(frequency);
        double exp = (27.55 - (20 * Math.Log10(frequencyInMHz)) + Math.Abs(signalStrength)) / 20.0;
        return Math.Pow(10.0, exp);
    }

    int FrequencyInMHz(long frequency)
        => (int)(frequency / Math.Pow(10, 6));
}
