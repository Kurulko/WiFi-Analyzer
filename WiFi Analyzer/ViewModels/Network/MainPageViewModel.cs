using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WiFi_Analyzer.Helpers;
using WiFi_Analyzer.Models;
using WiFi_Analyzer.Services.ConnectedNetwork;
using WiFi_Analyzer.Services.SpeedTest;
using WiFi_Analyzer.ViewModels.Network;

namespace WiFi_Analyzer.ViewModels;

public class MainPageViewModel : NetworkViewModel
{
    readonly ISpeedTestService speedTestService;

    DownloadSpeed? downloadSpeed;
    public DownloadSpeed? DownloadSpeed
    {
        get => downloadSpeed;
        set
        {
            downloadSpeed = value;
            OnPropertyChanged(nameof(DownloadSpeed));
        }
    }

    bool isBusy;
    public bool IsBusy
    {
        get => isBusy;
        set
        {
            isBusy = value;
            OnPropertyChanged(nameof(IsBusy));
        }
    }

    public ICommand GetDownloadSpeedCommand => new Command(async () => await GetDownloadSpeedAsync());
    public async Task GetDownloadSpeedAsync()
    {
        try
        {
            DownloadSpeed = null;
            IsBusy = true;
            DownloadSpeed = await speedTestService.GetDownloadSpeedAsync();
            IsBusy = false;
        }
        catch (Exception ex)
        {
            await ErrorHandler.DisplayErrorAsync(ex.Message);
        }
    }


    public MainPageViewModel(IConnectedNetworkService connectedNetworkService, ISpeedTestService speedTestService) : base(connectedNetworkService)
        => this.speedTestService = speedTestService;

    protected override async Task GetDataAsync()
    {
        ConnectedNetwork = connectedNetworkService.GetConnectedWiFiNetwork();
        await Task.CompletedTask;
    }
}