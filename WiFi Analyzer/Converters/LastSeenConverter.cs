using System.Globalization;

namespace WiFi_Analyzer.Converters;

public class LastSeenConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is DateTime lastSeen)
        {
            TimeSpan difference = DateTime.Now.Subtract(lastSeen);

            bool isThisYear = difference.TotalDays <= 365;
            bool isThisMonth = difference.TotalDays <= 31;
            bool isThisWeek = difference.TotalDays <= 7;
            bool isThisDay = difference.TotalHours <= 24;
            bool isThisHour = difference.TotalMinutes <= 60;
            bool isRecently = difference.TotalMinutes <= 5;
            bool isNow = difference.TotalMinutes <= 1;

            string lastSeenStr;

            if (isNow)
                lastSeenStr = "now";
            else if (isRecently)
                lastSeenStr = "recently";
            else if (isThisHour)
                lastSeenStr = "this hour";
            else if (isThisDay)
                lastSeenStr = "today";
            else if (isThisWeek)
                lastSeenStr = "this week";
            else if (isThisMonth)
                lastSeenStr = "this month";
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
