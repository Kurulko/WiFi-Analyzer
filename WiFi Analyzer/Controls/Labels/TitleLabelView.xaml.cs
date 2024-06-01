namespace WiFi_Analyzer.Controls;

public partial class TitleLabelView : ContentView
{
    public static readonly BindableProperty TextProperty = BindableProperty.Create(
           nameof(Text), typeof(string), typeof(TitleLabelView), string.Empty);

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public TitleLabelView()
        => InitializeComponent();
}