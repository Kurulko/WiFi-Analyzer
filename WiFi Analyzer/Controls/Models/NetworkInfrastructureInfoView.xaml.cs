using WiFi_Analyzer.Models;

namespace WiFi_Analyzer.Controls;

public partial class NetworkInfrastructureInfoView : ContentView
{
    public static readonly BindableProperty NetworkInfrastructureInfoProperty = BindableProperty.Create(
        nameof(NetworkInfrastructureInfo), typeof(NetworkInfrastructureInfo), typeof(NetworkInfrastructureInfoView));

    public NetworkInfrastructureInfo NetworkInfrastructureInfo
    {
        get => (NetworkInfrastructureInfo)GetValue(NetworkInfrastructureInfoProperty);
        set => SetValue(NetworkInfrastructureInfoProperty, value);
    }

    public NetworkInfrastructureInfoView()
		=> InitializeComponent();
}