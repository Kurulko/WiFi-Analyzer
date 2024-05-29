using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WiFi_Analyzer.Helpers;

public static class SingalColorHelper
{
    public static string GetHexColorBySingalStrength(int signalStrengthIndBm)
    {
        if (signalStrengthIndBm >= -30) // Strong signal (Green)
            return "#00FF00";
        else if (signalStrengthIndBm > -70) // Medium signal (Light Green)
            return "#90EE90";
        else if (signalStrengthIndBm > -80) // Weak signal (Yellow)
            return "#FFFF00";
        else // Very weak signal (Red)
            return "#FF0000";
    }
}
