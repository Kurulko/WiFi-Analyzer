using SpeedTest.Net.Enums;
using WiFi_Analyzer.Enums;

namespace WiFi_Analyzer.Extensions;

public static class StringExtensions
{
    public static SpeedTestUnit ParseToSpeedTestUnit(this string speedTestUnitStr)
       => speedTestUnitStr switch
       {
           "Bps" => SpeedTestUnit.BytesPerSecond,
           "KBps" or "KB/s" => SpeedTestUnit.KiloBytesPerSecond,
           "Kbps" or "Kb/s" => SpeedTestUnit.KiloBitsPerSecond,
           "MBps" or "MB/s" => SpeedTestUnit.MegaBytesPerSecond,
           "Mbps" or "Mb/s" => SpeedTestUnit.MegaBitsPerSecond,
           _ => throw new ArgumentException("Can't parse to SpeedTestUnit")
       };

    public static string ToShortString(this SpeedTestUnit speedTestUnit)
       => speedTestUnit switch
       {
           SpeedTestUnit.BytesPerSecond => "Bps",
           SpeedTestUnit.KiloBytesPerSecond => "KB/s",
           SpeedTestUnit.KiloBitsPerSecond => "Kb/s",
           SpeedTestUnit.MegaBytesPerSecond => "MB/s",
           SpeedTestUnit.MegaBitsPerSecond => "Mb/s",
           _ => speedTestUnit.ToString()
       };


    public static OrderBy ParseToOrderBy(this string orderByStr)
       => orderByStr?.ToLower() switch
       {
           "ascending" or "asc" => OrderBy.Ascending,
           "descending" or "desc" => OrderBy.Descending,
           _ => throw new ArgumentException("Can't parse to OrderBy")
       };

    public static bool TryParseToOrderBy(this string? orderByStr, out OrderBy? orderBy)
    {
        try
        {
            orderBy = ParseToOrderBy(orderByStr!);
            return true;
        }
        catch
        {
            orderBy = default;
            return false;
        }
    }

    public static string MacAddressToString(this byte[] macAddress)
        => string.Join(":", macAddress.Select(b => b.ToString("X2")));
}