using WiFi_Analyzer.Helpers;
using WiFi_Analyzer.Models;
using WiFi_Analyzer.Services.ConnectedNetwork;

namespace WiFi_Analyzer.Pages;

public partial class ConnectedNetworkPage : ContentPage
{
    readonly IConnectedNetworkService connectedNetworkService;

    public WiFiNetwork WiFiNetwork { get; private set; } = null!;

    public ConnectedNetworkPage()
    {
        connectedNetworkService = ServiceHelper.GetService<IConnectedNetworkService>()!;
        InitializeComponent();
        GetConnectedNetworkDetails();
        BindingContext = this;
    }

    async void GetConnectedNetworkDetails()
    {
        try
        {
            WiFiNetwork = connectedNetworkService.GetConnectedWiFiNetwork();
            WiFiNetwork.IPAddressInfo = connectedNetworkService.GetConnectedIPAddressInfo();
            WiFiNetwork.NetworkSecurityInfo = connectedNetworkService.GetConnectedNetworkSecurityInfo();
            WiFiNetwork.NetworkInfrastructureInfo = connectedNetworkService.GetConnectedNetworkInfrastructureInfo();
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
        }
    }
}