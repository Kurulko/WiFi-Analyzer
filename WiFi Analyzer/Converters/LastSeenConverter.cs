using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WiFi_Analyzer.Helpers;

namespace WiFi_Analyzer.Converters;

public class LastSeenConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is DateTime lastSeen)
        {
            DateTime now =  DateTime.Now;

            bool isThisYear = now.Year == lastSeen.Year;
            bool isThisMonth = isThisYear && now.Month == lastSeen.Month;
            bool isThisDay = isThisMonth & now.Day == lastSeen.Day;
            bool isThisHour = isThisDay & now.Hour == lastSeen.Hour;
            bool isThisMinute = isThisHour & now.Minute == lastSeen.Minute;

            string lastSeenStr = string.Empty;

            if (isThisMinute)
                lastSeenStr = "now";
            else if (isThisHour)
                lastSeenStr = "this hour";
            else if (isThisDay)
                lastSeenStr = "this day";
            else if (isThisMonth)
                lastSeenStr = "this mont";
            else if (isThisYear)
                lastSeenStr = "this year";
            else
                lastSeenStr = "long ago";

            return lastSeenStr;
        }

        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
