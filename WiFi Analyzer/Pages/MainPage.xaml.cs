using SpeedTest.Net.Enums;
using SpeedTest.Net;
using WiFi_Analyzer.Models;
using WiFi_Analyzer.Services.ConnectedNetwork;
using System.Windows.Input;
using WiFi_Analyzer.Services.SpeedTest;
using WiFi_Analyzer.Helpers;
using WiFi_Analyzer.ViewModels;

namespace WiFi_Analyzer.Pages;

public partial class MainPage : ContentPage
{
    readonly MainPageViewModel mainPageViewModel = null!;

    public MainPage()
    {
        mainPageViewModel = ServiceHelper.GetService<MainPageViewModel>()!;

        InitializeComponent();

        BindingContext = mainPageViewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await mainPageViewModel.LoadDataAsync();
    }
}
