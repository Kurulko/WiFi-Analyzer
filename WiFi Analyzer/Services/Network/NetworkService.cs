using NativeWifi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NativeWifi.Wlan;

namespace WiFi_Analyzer.Services;

public abstract class NetworkService
{
    protected string GetStringForSSID(Dot11Ssid ssid)
        => Encoding.ASCII.GetString(ssid.SSID, 0, (int)ssid.SSIDLength);

    protected string FindProtocolString(Dot11PhyType type)
    {
        Dictionary<int, string> phyTypes = new Dictionary<int, string>()
        {
            { 1, "802.11b" }, // DSSS
            { 2, "802.11a" }, // OFDM
            { 3, "802.11g" }, // ERP
            { 4, "802.11n" }, // HT
            { 5, "802.11ac" }, // VHT
            { 6, "802.11ad" }, // DMG
            { 7, "802.11aj" }, // China Millimeter Wave
            { 8, "802.11ax" }, // HE
            { 9, "802.11ay" }, // Next Generation 60 GHz
            { 10, "802.11az" }, // Next Generation Positioning
        };
        int typeInt = (int)type;
        string phyTypeString = phyTypes.ContainsKey(typeInt) ? phyTypes[typeInt] : "Unknown";
        return phyTypeString;
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
