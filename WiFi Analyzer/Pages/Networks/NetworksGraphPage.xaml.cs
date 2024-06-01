using Microcharts.Maui;
using Microcharts;
using SkiaSharp;
using WiFi_Analyzer.Models;
using WiFi_Analyzer.Services.Networks;
using System.Windows.Input;
using System;
using WiFi_Analyzer.Helpers;
using WiFi_Analyzer.ViewModels;

namespace WiFi_Analyzer.Pages.Networks;

public partial class NetworksGraphPage : ContentPage
{

    readonly NetworksGraphViewModel networksGraphViewModel = null!;

    public NetworksGraphPage()
    {
        networksGraphViewModel = ServiceHelper.GetService<NetworksGraphViewModel>()!;

        InitializeComponent();

        BindingContext = networksGraphViewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        UpdateNetworks();
    }

    async void UpdateNetworks()
         => await networksGraphViewModel.LoadDataAsync();

    async void NavigateToTablePage(object sender, EventArgs e)
        => await Navigation.PushAsync(new NetworksTablePage());

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        networksGraphViewModel.Dispose();
    }
}