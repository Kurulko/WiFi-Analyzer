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
using WiFi_Analyzer.Services.Networks;
using WiFi_Analyzer.ViewModels.Network;

namespace WiFi_Analyzer.ViewModels;

public class ConnectedNetworkViewModel : NetworkViewModel
{
    IPAddressInfo? _IPAddressInfo;
    public IPAddressInfo? IPAddressInfo
    {
        get => _IPAddressInfo;
        set
        {
            _IPAddressInfo = value;
            OnPropertyChanged(nameof(IPAddressInfo));
        }
    }

    NetworkSecurityInfo? networkSecurityInfo;
    public NetworkSecurityInfo? NetworkSecurityInfo
    {
        get => networkSecurityInfo;
        set
        {
            networkSecurityInfo = value;
            OnPropertyChanged(nameof(NetworkSecurityInfo));
        }
    }

    NetworkInfrastructureInfo? networkInfrastructureInfo;
    public NetworkInfrastructureInfo? NetworkInfrastructureInfo
    {
        get => networkInfrastructureInfo;
        set
        {
            networkInfrastructureInfo = value;
            OnPropertyChanged(nameof(NetworkInfrastructureInfo));
        }
    }

    public ConnectedNetworkViewModel(IConnectedNetworkService connectedNetworkService, INetworksService networksService) : base(connectedNetworkService, networksService) { }

    protected override async Task GetDataAsync()
    {
        //ConnectedNetwork = connectedNetworkService.GetConnectedWiFiNetwork();
        await base.GetDataAsync();

        IPAddressInfo = await connectedNetworkService.GetConnectedIPAddressInfo();
        NetworkSecurityInfo = connectedNetworkService.GetConnectedNetworkSecurityInfo();
        NetworkInfrastructureInfo = connectedNetworkService.GetConnectedNetworkInfrastructureInfo();
    }
}
