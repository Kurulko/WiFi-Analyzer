using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WiFi_Analyzer.Models;
using WiFi_Analyzer.Services.ConnectedNetwork;

namespace WiFi_Analyzer.ViewModels.Network;

public abstract class NetworkViewModel : ViewModelBase
{
    protected readonly IConnectedNetworkService connectedNetworkService;

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

    public NetworkViewModel(IConnectedNetworkService connectedNetworkService)
        => this.connectedNetworkService = connectedNetworkService;

    protected override Task GetDataAsync()
    {
        ConnectedNetwork = connectedNetworkService.GetConnectedWiFiNetwork();
        return Task.CompletedTask;
    }
}
