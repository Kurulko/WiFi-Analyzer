using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
