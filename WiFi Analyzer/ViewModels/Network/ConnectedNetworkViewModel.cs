using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WiFi_Analyzer.Helpers;
using WiFi_Analyzer.Models;
using WiFi_Analyzer.Services.ConnectedNetwork;
using WiFi_Analyzer.ViewModels.Network;

namespace WiFi_Analyzer.ViewModels;

public class ConnectedNetworkViewModel : NetworkViewModel
{
    public ConnectedNetworkViewModel(IConnectedNetworkService connectedNetworkService) : base(connectedNetworkService) { }

    protected override async Task GetDataAsync()
    {
        //ConnectedNetwork = connectedNetworkService.GetConnectedWiFiNetwork();
        await base.GetDataAsync();

        ConnectedNetwork!.IPAddressInfo = await connectedNetworkService.GetConnectedIPAddressInfo();
        ConnectedNetwork.NetworkSecurityInfo = connectedNetworkService.GetConnectedNetworkSecurityInfo();
        ConnectedNetwork.NetworkInfrastructureInfo = connectedNetworkService.GetConnectedNetworkInfrastructureInfo();
    }
}
