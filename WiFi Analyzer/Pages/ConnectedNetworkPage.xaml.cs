using WiFi_Analyzer.Helpers;
using WiFi_Analyzer.Models;
using WiFi_Analyzer.Services.ConnectedNetwork;
using WiFi_Analyzer.ViewModels;

namespace WiFi_Analyzer.Pages;

public partial class ConnectedNetworkPage : ContentPage
{
    readonly ConnectedNetworkViewModel connectedNetworkViewModel = null!;

    public ConnectedNetworkPage()
    {
        connectedNetworkViewModel = ServiceHelper.GetService<ConnectedNetworkViewModel>()!;

        InitializeComponent();

        BindingContext = connectedNetworkViewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await connectedNetworkViewModel.LoadDataAsync();
    }
}