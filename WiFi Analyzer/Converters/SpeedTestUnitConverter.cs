using SpeedTest.Net.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WiFi_Analyzer.Extensions;

namespace WiFi_Analyzer.Converters;

public class SpeedTestUnitConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        => value is SpeedTestUnit unit ? unit.ToShortString() : string.Empty;

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => value is string valueStr ? valueStr.ParseToSpeedTestUnit() : default;
}