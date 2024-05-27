using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NativeWifi.Wlan;

namespace WiFi_Analyzer.Models;

public class WiFiNetwork
{
    public string SSID { get; set; } = null!;

    public long FrequencyInHz { get; set; }
    public int FrequencyInkHz => (int)(FrequencyInHz / Math.Pow(10, 3));
    public int FrequencyInMHz => (int)(FrequencyInHz / Math.Pow(10, 6));
    public int FrequencyInGHz => (int)(FrequencyInHz / Math.Pow(10, 9));

    public bool IsConnected { get; set; }
    public int Channel { get; set; }

    public int SignalStrengthIndBm { get; set; }
    public int SignalStrengthInPercentage { 
        get {
            const int minRssi = -100; // Minimum RSSI value
            if (SignalStrengthIndBm <= minRssi) 
                return 0;

            const int maxRssi = -50;  // Maximum RSSI value
            if (SignalStrengthIndBm >= maxRssi) 
                return 100;

            return 2 * (SignalStrengthIndBm + 100);
        } 
    }

    public byte[] MacAddress { get; set; } = null!;
    public string StringMacAddress {
        get => string.Join(":", MacAddress.Select(b => b.ToString("X2"))); 
    }

    public double DistanceInMeters { get; set; }

    public bool IsSecured { get; set; }
    public Dot11AuthAlgorithm AuthenticationAlgorithm { get; set; }

    public IPAddressInfo? IPAddressInfo { get; set; }
    public NetworkSecurityInfo? NetworkSecurityInfo { get; set; }
    public NetworkInfrastructureInfo? NetworkInfrastructureInfo { get; set; }
}
