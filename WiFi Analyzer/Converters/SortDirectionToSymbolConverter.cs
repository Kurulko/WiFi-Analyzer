using System.Globalization;
using WiFi_Analyzer.Enums;

namespace WiFi_Analyzer.Converters;

public class SortDirectionToSymbolConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is OrderBy sortDirection)
            return sortDirection == OrderBy.Ascending ? "↑" : "↓";

        return null!;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}