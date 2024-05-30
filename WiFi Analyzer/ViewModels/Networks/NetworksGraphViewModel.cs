using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WiFi_Analyzer.Helpers;
using WiFi_Analyzer.Models;
using WiFi_Analyzer.Services.Networks;

namespace WiFi_Analyzer.ViewModels;

public class NetworksGraphViewModel : NetworksViewModel
{
    public bool HasNoFilteredNetworks => !HasFilteredNetworks;

    public override IEnumerable<WiFiNetwork> FilteredWiFiNetworks
    {
        get => filteredWiFiNetworks;
        set
        {
            filteredWiFiNetworks = value;
            OnPropertyChanged(nameof(FilteredWiFiNetworks));
            OnPropertyChanged(nameof(HasFilteredNetworks));
            OnPropertyChanged(nameof(HasNoFilteredNetworks));
        }
    }

    public NetworksGraphViewModel(INetworksService networksService) : base(networksService)
    {
    }

    readonly SKColor backgroundColor = SKColor.Parse("#000000");
    readonly TimeSpan animationDuration = new (0, 0, 2);
    readonly SKColor labelColor = SKColor.Parse("#FFFFFF");

    public Chart GetDBmChartView()
       => new LineChart()
       {
           Entries = GetDBMEntries(),
           BackgroundColor = backgroundColor,
           AnimationDuration = animationDuration,
           LabelColor = labelColor,
           LabelOrientation = Orientation.Horizontal,
           ValueLabelOrientation = Orientation.Horizontal,
       };

    public Chart GetDistanceChartView()
        => new RadarChart()
        {
            Entries = GetDistanceEntries(),
            BackgroundColor = backgroundColor,
            AnimationDuration = animationDuration,
            LabelColor = labelColor,
        };


    IEnumerable<ChartEntry> GetDBMEntries()
    {
        IList<ChartEntry> entries = new List<ChartEntry>();

        foreach (WiFiNetwork network in FilteredWiFiNetworks.OrderBy(n => n.Channel))
        {
            int signalStrength = network.NetworkStates!.SignalStrengthIndBm;
            SKColor color = GetColorBySignalStrength(signalStrength);

            entries.Add(new ChartEntry(signalStrength)
            {
                Label = $"{network.SSID} (Ch.{network.Channel})",
                ValueLabel = $"{signalStrength} dBm",
                ValueLabelColor = color,
                Color = color
            });
        }

        return entries;
    }


    IEnumerable<ChartEntry> GetDistanceEntries()
    {
        IList<ChartEntry> entries = new List<ChartEntry>();

        foreach (var network in FilteredWiFiNetworks)
        {
            NetworkStates metworkStates = network.NetworkStates!;
            SKColor color = GetColorBySignalStrength(metworkStates.SignalStrengthIndBm);

            double distance = metworkStates.DistanceInMeters;

            entries.Add(new ChartEntry((float)distance)
            {
                Label = network.SSID,
                ValueLabel = $"{Math.Round(distance, 2)} m",
                ValueLabelColor = color,
                Color = color
            });
        }

        return entries;
    }

    SKColor GetColorBySignalStrength(int signalStrengthIndBm)
        => SKColor.Parse(SingalColorHelper.GetHexColorBySingalStrength(signalStrengthIndBm));
}
