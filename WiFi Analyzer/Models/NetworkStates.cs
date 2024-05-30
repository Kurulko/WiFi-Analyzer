using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WiFi_Analyzer.Models;

public class NetworkStates
{
    public bool IsConnected { get; set; }

    public int SignalStrengthIndBm { get; set; }
    public int SignalStrengthInPercentage
    {
        get
        {
            const int minRssi = -100; // Minimum RSSI value
            if (SignalStrengthIndBm <= minRssi)
                return 0;

            const int maxRssi = -50;  // Maximum RSSI value
            if (SignalStrengthIndBm >= maxRssi)
                return 100;

            return 2 * (SignalStrengthIndBm + 100);
        }
    }

    public double DistanceInMeters { get; set; }
}