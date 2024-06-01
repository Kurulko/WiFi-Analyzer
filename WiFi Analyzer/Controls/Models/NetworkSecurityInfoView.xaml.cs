using WiFi_Analyzer.Models;

namespace WiFi_Analyzer.Controls;

public partial class NetworkSecurityInfoView : ContentView
{
    public static readonly BindableProperty NetworkSecurityInfoProperty = BindableProperty.Create(
        nameof(NetworkSecurityInfo), typeof(NetworkSecurityInfo), typeof(NetworkSecurityInfoView));

    public NetworkSecurityInfo NetworkSecurityInfo
    {
        get => (NetworkSecurityInfo)GetValue(NetworkSecurityInfoProperty);
        set => SetValue(NetworkSecurityInfoProperty, value);
    }

    public NetworkSecurityInfoView()
		=> InitializeComponent();
}