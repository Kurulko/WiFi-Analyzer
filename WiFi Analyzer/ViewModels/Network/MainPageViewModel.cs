﻿using System.Windows.Input;
using WiFi_Analyzer.Helpers;
using WiFi_Analyzer.Models;
using WiFi_Analyzer.Services.ConnectedNetwork;
using WiFi_Analyzer.Services.Networks;
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

    public MainPageViewModel(IConnectedNetworkService connectedNetworkService, ISpeedTestService speedTestService, INetworksService networksService) : base(connectedNetworkService, networksService)
            => this.speedTestService = speedTestService;


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
}