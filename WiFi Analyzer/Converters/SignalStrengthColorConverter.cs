using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WiFi_Analyzer.Converters;

public class SignalStrengthColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is int signalStrength)
        {
            if (signalStrength >= -30)
            {
                return Colors.Green; // Strong signal
            }
            else if (signalStrength > -70)
            {
                return Colors.LightGreen; // Medium signal
            }
            else if (signalStrength > -80)
            {
                return Colors.Yellow; // Weak signal
            }
            else
            {
                return Colors.Red; // Very weak signal
            }            
        }
        return value; // Return the original value if not a double
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}