using WiFi_Analyzer.Models;

namespace WiFi_Analyzer.Services.Networks;

public interface INetworksService
{
    Task UpdateWiFiNetworksAsync();

    Task<IEnumerable<WiFiNetwork>> GetWiFiNetworksAsync();
    Task<IEnumerable<WiFiNetwork>> GetWiFiNetworksWithStatesAsync();

    Task AddWiFiNetworkAsync(WiFiNetwork network);
    Task UpdateWiFiNetworkAsync(WiFiNetwork network);

    Task DeleteWiFiNetworkByIdAsync(long networkId);
    Task DeleteWiFiNetworkByBSSIDAsync(string BSSID);

    Task<WiFiNetwork?> GetWiFiNetworkByIdAsync(long id);
    Task<WiFiNetwork?> GetWiFiNetworkByBSSIDAsync(string BSSID);

    NetworkStates? GetNetworkStates(WiFiNetwork network);
}
