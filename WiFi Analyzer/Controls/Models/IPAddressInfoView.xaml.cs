using WiFi_Analyzer.Models;

namespace WiFi_Analyzer.Controls;

public partial class IPAddressInfoView : ContentView
{
    public static readonly BindableProperty IPAddressInfoProperty = BindableProperty.Create(
        nameof(IPAddressInfo), typeof(IPAddressInfo), typeof(IPAddressInfoView), null);

    public IPAddressInfoView()
		=> InitializeComponent();

    public IPAddressInfo? IPAddressInfo
    {
        get => (IPAddressInfo?)GetValue(IPAddressInfoProperty);
        set => SetValue(IPAddressInfoProperty, value);
    }
}