using Microcharts;
using SkiaSharp;
using WiFi_Analyzer.Helpers;
using WiFi_Analyzer.Models;
using WiFi_Analyzer.Services.Networks;

namespace WiFi_Analyzer.ViewModels;

public class NetworksGraphViewModel : NetworksViewModel
{
    public bool HasNoFilteredNetworks => !HasFilteredNetworks;

    Chart dBmChart = null!;
    public Chart DBmChart
    {
        get => dBmChart;
        set
        {
            dBmChart = value;
            OnPropertyChanged(nameof(DBmChart));
        }
    }

    Chart distanceChart = null!;
    public Chart DistanceChart
    {
        get => distanceChart;
        set
        {
            distanceChart = value;
            OnPropertyChanged(nameof(DistanceChart));
        }
    }

    public override IEnumerable<WiFiNetwork> FilteredWiFiNetworks
    {
        get => filteredWiFiNetworks;
        set
        {
            filteredWiFiNetworks = value;
            OnPropertyChanged(nameof(FilteredWiFiNetworks));
            OnPropertyChanged(nameof(HasFilteredNetworks));
            OnPropertyChanged(nameof(HasNoFilteredNetworks));
            UpdateCharts();
        }
    }

    public NetworksGraphViewModel(INetworksService networksService) : base(networksService)
    {
    }

    void UpdateCharts()
    {
        if (HasFilteredNetworks)
        {
            DBmChart = GetDBmChartView();
            DistanceChart = GetDistanceChartView();
        }
        else
        {
            DBmChart = null!; 
            DistanceChart = null!; 
        }
    }

    readonly SKColor backgroundColor = SKColor.Parse("#000000");
    readonly TimeSpan animationDuration = new (0, 0, 2);
    readonly SKColor labelColor = SKColor.Parse("#FFFFFF");

    public Chart GetDBmChartView()
    {
        var entries = GetDBMEntries();
        Chart chart = entries.Count() == 1 ?
            new PointChart()
            {
                Entries = entries,
                BackgroundColor = backgroundColor,
                AnimationDuration = animationDuration,
                LabelColor = labelColor,
                LabelOrientation = Orientation.Horizontal,
                ValueLabelOrientation = Orientation.Horizontal,
            } :
            new LineChart()
            {
                Entries = entries,
                BackgroundColor = backgroundColor,
                AnimationDuration = animationDuration,
                LabelColor = labelColor,
                LabelOrientation = Orientation.Horizontal,
                ValueLabelOrientation = Orientation.Horizontal,
            };
        return chart;
    }

    public Chart GetDistanceChartView()
    {
        var entries = GetDistanceEntries();
        Chart chart = entries.Count() == 1 ?
            new PointChart() {
                Entries = entries,
                BackgroundColor = backgroundColor,
                AnimationDuration = animationDuration,
                LabelColor = labelColor,
                LabelOrientation = Orientation.Horizontal,
                ValueLabelOrientation = Orientation.Horizontal,
            } :
            new RadarChart() {
                Entries = entries,
                BackgroundColor = backgroundColor,
                AnimationDuration = animationDuration,
                LabelColor = labelColor,
            };
        return chart;
    }

    IEnumerable<ChartEntry> GetDBMEntries()
    {
        IList<ChartEntry> entries = new List<ChartEntry>();

        foreach (WiFiNetwork network in FilteredWiFiNetworks.OrderBy(n => n.Channel))
        {
            NetworkStates? networkStates = network.NetworkStates;
            if (networkStates is not null)
            {
                int signalStrength = networkStates.SignalStrengthIndBm;
                SKColor color = GetColorBySignalStrength(signalStrength);

                entries.Add(new ChartEntry(signalStrength)
                {
                    Label = $"{network.SSID} (Ch.{network.Channel})",
                    ValueLabel = $"{signalStrength} dBm",
                    ValueLabelColor = color,
                    Color = color
                });
            }
        }

        return entries;
    }

    IEnumerable<ChartEntry> GetDistanceEntries()
    {
        IList<ChartEntry> entries = new List<ChartEntry>();

        foreach (var network in FilteredWiFiNetworks)
        {
            NetworkStates? networkStates = network.NetworkStates;
            if (networkStates is not null)
            {
                SKColor color = GetColorBySignalStrength(networkStates.SignalStrengthIndBm);

                double distance = networkStates.DistanceInMeters;

                entries.Add(new ChartEntry((float)distance)
                {
                    Label = network.SSID,
                    ValueLabel = $"{Math.Round(distance, 2)} m",
                    ValueLabelColor = color,
                    Color = color
                });
            }
        }

        return entries;
    }

    SKColor GetColorBySignalStrength(int signalStrengthIndBm)
        => SKColor.Parse(SingalColorHelper.GetHexColorBySingalStrength(signalStrengthIndBm));
}
