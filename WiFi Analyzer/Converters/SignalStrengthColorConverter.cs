using System.Globalization;
using WiFi_Analyzer.Helpers;

namespace WiFi_Analyzer.Converters;

public class SignalStrengthColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is int signalStrength)
            return Color.FromHex(SingalColorHelper.GetHexColorBySingalStrength(signalStrength));
        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}