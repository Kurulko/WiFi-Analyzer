using WiFi_Analyzer.Models;

namespace WiFi_Analyzer.Helpers;

public static class NetworksFilter
{
    const double GHz_5 = 5;
    const double GHz_6 = 6;

    public static IEnumerable<WiFiNetwork> FilterByGHz(IEnumerable<WiFiNetwork> networks, string parameter)
        => (parameter switch {
            "2.4 GHz" => networks.Where(n => n.FrequencyInGHz < GHz_5),
            "5 GHz" => networks.Where(n => n.FrequencyInGHz >= GHz_5 && n.FrequencyInGHz < GHz_6),
            "6 GHz" => networks.Where(n => n.FrequencyInGHz >= GHz_6),
            "All" or _ => networks,
        }).ToList();
}
