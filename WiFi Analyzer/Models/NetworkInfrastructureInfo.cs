using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace WiFi_Analyzer.Models;

public class NetworkInfrastructureInfo
{
    public string Interface { get; set; } = null!;
    public NetworkInterfaceType InterfaceType { get; set; }
    public OperationalStatus OperationalStatus { get; set; }
}
