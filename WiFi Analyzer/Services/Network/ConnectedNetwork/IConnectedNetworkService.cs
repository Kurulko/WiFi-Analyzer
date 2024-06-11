using WiFi_Analyzer.Models;

namespace WiFi_Analyzer.Services.ConnectedNetwork;

public interface IConnectedNetworkService
{
    WiFiNetwork GetConnectedWiFiNetwork();
    NetworkStates GetConnectedNetworkStates();
    Task<IPAddressInfo> GetConnectedIPAddressInfo();
    NetworkSecurityInfo GetConnectedNetworkSecurityInfo();
    NetworkInfrastructureInfo GetConnectedNetworkInfrastructureInfo();
}
