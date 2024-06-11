using System.Net.NetworkInformation;

namespace WiFi_Analyzer.Models;

public class NetworkInfrastructureInfo
{
    public string Interface { get; set; } = null!;
    public NetworkInterfaceType InterfaceType { get; set; }
    public OperationalStatus OperationalStatus { get; set; }
}
