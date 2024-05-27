using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WiFi_Analyzer.Models;

namespace WiFi_Analyzer.Services.Networks;

public interface INetworksService
{
    IEnumerable<WiFiNetwork> GetWiFiNetworks();
}
