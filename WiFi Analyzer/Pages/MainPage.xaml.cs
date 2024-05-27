using SpeedTest.Net.Enums;
using SpeedTest.Net;
using WiFi_Analyzer.Models;
using WiFi_Analyzer.Services.ConnectedNetwork;
using System.Windows.Input;
using WiFi_Analyzer.Services.SpeedTest;
using WiFi_Analyzer.Helpers;

namespace WiFi_Analyzer.Pages;

public partial class MainPage : ContentPage
{
    readonly IConnectedNetworkService connectedNetworkService;
    readonly ISpeedTestService speedTestService;

    public WiFiNetwork WiFiNetwork { get; private set; } = null!;

    public DownloadSpeed? DownloadSpeed { get; set; }
    public ICommand GetDownloadSpeedCommand { get; }

    public MainPage()
    {
        connectedNetworkService = ServiceHelper.GetService<IConnectedNetworkService>()!;
        speedTestService = ServiceHelper.GetService<ISpeedTestService>()!;

        GetDownloadSpeedCommand = new Command(async () => await GetDownloadSpeedAsync());

        InitializeComponent();
        GetConnectedNetworkDetails();

        BindingContext = this;
    }

    async void GetConnectedNetworkDetails()
    {
        try
        {
            WiFiNetwork = connectedNetworkService.GetConnectedWiFiNetwork();
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
        }
    }

    public async Task GetDownloadSpeedAsync()
    {
        try
        {
            DownloadSpeed = null;
            IsBusy = true;
            DownloadSpeed = await speedTestService.GetDownloadSpeedAsync();
            OnPropertyChanged(nameof(DownloadSpeed));
            IsBusy = false;
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
        }
    }
}
