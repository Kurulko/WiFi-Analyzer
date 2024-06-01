using WiFi_Analyzer.Models;

namespace WiFi_Analyzer.Controls;

public partial class HeaderLabelView : ContentView
{
    public static readonly BindableProperty TextProperty = BindableProperty.Create(
        nameof(Text), typeof(string), typeof(HeaderLabelView), string.Empty);

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public HeaderLabelView()
        => InitializeComponent();
}