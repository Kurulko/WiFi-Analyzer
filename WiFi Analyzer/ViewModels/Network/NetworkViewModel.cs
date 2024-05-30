using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WiFi_Analyzer.Models;
using WiFi_Analyzer.Services.ConnectedNetwork;
using WiFi_Analyzer.Services.Networks;

namespace WiFi_Analyzer.ViewModels.Network;

public abstract class NetworkViewModel : ViewModelBase
{
    protected readonly IConnectedNetworkService connectedNetworkService;
    protected readonly INetworksService networksService;

    WiFiNetwork? connectedNetwork;
    public WiFiNetwork? ConnectedNetwork
    {
        get => connectedNetwork;
        set
        {
            connectedNetwork = value;
            OnPropertyChanged(nameof(ConnectedNetwork));
        }
    }

    NetworkStates? networkStates;
    public NetworkStates? NetworkStates
    {
        get => networkStates;
        set
        {
            networkStates = value;
            OnPropertyChanged(nameof(NetworkStates));
        }
    }

    public NetworkViewModel(IConnectedNetworkService connectedNetworkService, INetworksService networksService)
        => (this.connectedNetworkService, this.networksService) = (connectedNetworkService, networksService);

    protected override async Task GetDataAsync()
    {
        ConnectedNetwork = connectedNetworkService.GetConnectedWiFiNetwork();
           
        await networksService.UpdateWiFiNetworkAsync(ConnectedNetwork);

        NetworkStates = connectedNetworkService.GetConnectedNetworkStates();
    }
}
