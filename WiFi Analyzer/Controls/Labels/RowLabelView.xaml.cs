namespace WiFi_Analyzer.Controls;

public partial class RowLabelView : ContentView
{
    public static readonly BindableProperty TextProperty = BindableProperty.Create(
           nameof(Text), typeof(string), typeof(RowLabelView), string.Empty);

    public static readonly BindableProperty TextColorProperty = BindableProperty.Create(
        nameof(TextColor), typeof(Color), typeof(RowLabelView), Color.FromHex("#FFFFFF"));

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public Color TextColor
    {
        get => (Color)GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }

    public RowLabelView()
        => InitializeComponent();
}