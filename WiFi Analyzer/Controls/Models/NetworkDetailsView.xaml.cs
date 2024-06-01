using WiFi_Analyzer.Models;

namespace WiFi_Analyzer.Controls;

public partial class NetworkDetailsView : ContentView
{
    public static readonly BindableProperty ConnectedNetworkProperty = BindableProperty.Create(
        nameof(ConnectedNetwork), typeof(WiFiNetwork), typeof(NetworkDetailsView));

    public static readonly BindableProperty NetworkStatesProperty = BindableProperty.Create(
        nameof(NetworkStates), typeof(NetworkStates), typeof(NetworkDetailsView));

    public WiFiNetwork ConnectedNetwork
    {
        get => (WiFiNetwork)GetValue(ConnectedNetworkProperty);
        set => SetValue(ConnectedNetworkProperty, value);
    }

    public NetworkStates NetworkStates
    {
        get => (NetworkStates)GetValue(NetworkStatesProperty);
        set => SetValue(NetworkStatesProperty, value);
    }

    public NetworkDetailsView()
        => InitializeComponent();
}