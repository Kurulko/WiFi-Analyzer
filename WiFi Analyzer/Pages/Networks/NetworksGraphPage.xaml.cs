using Microcharts.Maui;
using Microcharts;
using SkiaSharp;
using WiFi_Analyzer.Models;
using WiFi_Analyzer.Services.Networks;
using System.Windows.Input;
using System;
using WiFi_Analyzer.Helpers;

namespace WiFi_Analyzer.Pages.Networks;

public partial class NetworksGraphPage : ContentPage
{
    readonly INetworksService networksService;
    IEnumerable<WiFiNetwork> wifiNetworks = null!;
    IEnumerable<WiFiNetwork> filteredWiFiNetworks = null!;

    public bool HasNetworks => filteredWiFiNetworks.Any();
    public bool HasNoNetworks => !HasNetworks;
    public ICommand FilterByGHzCommand => new Command<string>(parameter => FilterByGHz(parameter));

    public NetworksGraphPage()
    {
        networksService = ServiceHelper.GetService<INetworksService>()!;
        GetNetworksDetails();

        InitializeComponent();

        SetDBmChartView();
        SetDistanceChartView();

        BindingContext = this;
    }

    void FilterByGHz(string parameter)
    {
        filteredWiFiNetworks = WiFiNetworksFilter.FilterByGHz(wifiNetworks, parameter);

        OnPropertyChanged(nameof(HasNetworks));
        OnPropertyChanged(nameof(HasNoNetworks));

        if (HasNetworks)
        {
            SetDBmChartView();
            SetDistanceChartView();
        }
    }

    readonly SKColor backgroundColor = SKColor.Parse("#000000");
    readonly TimeSpan animationDuration = new TimeSpan(0, 0, 2);
    readonly SKColor labelColor = SKColor.Parse("#FFFFFF");

    void SetDBmChartView()
    {
        dBmChartView.Chart = new LineChart()
        {
            Entries = GetDBMEntries(),
            BackgroundColor = backgroundColor,
            AnimationDuration = animationDuration,
            LabelColor = labelColor,
            LabelOrientation = Orientation.Horizontal,
            ValueLabelOrientation = Orientation.Horizontal,
        };
    }

    void SetDistanceChartView()
    {
        distanceChartView.Chart = new RadarChart()
        {
            Entries = GetDistanceEntries(),
            BackgroundColor = backgroundColor,
            AnimationDuration = animationDuration,
            LabelColor = labelColor,
        };
    }


    IEnumerable<ChartEntry> GetDBMEntries()
    {
        IList<ChartEntry> entries = new List<ChartEntry>();

        foreach (WiFiNetwork network in filteredWiFiNetworks.OrderBy(n => n.Channel))
        {
            SKColor color = GetColorBySignalStrength(network.SignalStrengthIndBm);

            entries.Add(new ChartEntry(network.SignalStrengthIndBm)
            {
                Label = $"{network.SSID} (Ch.{network.Channel})",
                ValueLabel = $"{network.SignalStrengthIndBm} dBm",
                ValueLabelColor = color,
                Color = color
            });
        }

        return entries;
    }


    IEnumerable<ChartEntry> GetDistanceEntries()
    {
        IList<ChartEntry> entries = new List<ChartEntry>();

        foreach (var network in filteredWiFiNetworks)
        {
            SKColor color = GetColorBySignalStrength(network.SignalStrengthIndBm);

            entries.Add(new ChartEntry((float)network.DistanceInMeters)
            {
                Label = network.SSID,
                ValueLabel = $"{Math.Round(network.DistanceInMeters, 2)} m",
                ValueLabelColor = color,
                Color = color
            });
        }

        return entries;
    }

    SKColor GetColorBySignalStrength(int signalStrengthIndBm)
    {
        SKColor color;

        if (signalStrengthIndBm >= -30) // Strong signal (Green)
            color = SKColor.Parse("#00FF00");
        else if (signalStrengthIndBm > -70) // Medium signal (Light Green)
            color = SKColor.Parse("#90EE90");
        else if (signalStrengthIndBm > -80) // Weak signal (Yellow)
            color = SKColor.Parse("#FFFF00");
        else // Very weak signal (Red)
            color = SKColor.Parse("#FF0000");

        return color;
    }


    async void NavigateToTablePage(object sender, EventArgs e)
        => await Navigation.PushAsync(new NetworksTablePage());

    async void GetNetworksDetails()
    {
        try
        {
            wifiNetworks = networksService.GetWiFiNetworks();
            filteredWiFiNetworks = wifiNetworks.ToList();
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
        }
    }

    void UpdateWiFiNetworks(object sender, EventArgs e)
    {
        GetNetworksDetails();

        if (HasNetworks)
        {
            SetDBmChartView();
            SetDistanceChartView();
        }

        OnPropertyChanged(nameof(HasNetworks));
        OnPropertyChanged(nameof(HasNoNetworks));
    }
}