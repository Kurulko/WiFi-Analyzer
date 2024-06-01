using Microsoft.Maui.Graphics.Text;
using System.Windows.Input;

namespace WiFi_Analyzer.Controls;

public partial class NetworksFilterByGHzView : ContentView
{
    public static readonly BindableProperty FilterByGHzCommandProperty = BindableProperty.Create(
        nameof(FilterByGHzCommand), typeof(ICommand), typeof(SpeedTestView));

    public ICommand FilterByGHzCommand
    {
        get => (ICommand)GetValue(FilterByGHzCommandProperty);
        set => SetValue(FilterByGHzCommandProperty, value);
    }

    public NetworksFilterByGHzView()
		=> InitializeComponent();

    readonly Color clickedButtonBackgroundColor = Color.FromHex("#808080");
    readonly Color unclickedButtonBackgroundColor = Color.FromHex("#FFFFFF");
    void OnGHzButtonClicked(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        button.BackgroundColor = clickedButtonBackgroundColor;

        string buttonText = button.Text;
        
        if (buttonText != twoDotFourGHzButton.Text)
            twoDotFourGHzButton.BackgroundColor = unclickedButtonBackgroundColor;
        if (buttonText != fiveGHzButton.Text)
            fiveGHzButton.BackgroundColor = unclickedButtonBackgroundColor;
        if (buttonText != sixGHzButton.Text)
            sixGHzButton.BackgroundColor = unclickedButtonBackgroundColor;
        if (buttonText != allGHzButton.Text)
            allGHzButton.BackgroundColor = unclickedButtonBackgroundColor;
    }
}