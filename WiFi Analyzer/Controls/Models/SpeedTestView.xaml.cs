using System.Windows.Input;
using WiFi_Analyzer.Models;

namespace WiFi_Analyzer.Controls;

public partial class SpeedTestView : ContentView
{
    public static readonly BindableProperty DownloadSpeedProperty = BindableProperty.Create(
        nameof(DownloadSpeed), typeof(DownloadSpeed), typeof(SpeedTestView));

    public static readonly BindableProperty IsBusyProperty = BindableProperty.Create(
        nameof(IsBusy), typeof(bool), typeof(SpeedTestView), false);

    public static readonly BindableProperty GetDownloadSpeedCommandProperty = BindableProperty.Create(
        nameof(GetDownloadSpeedCommand), typeof(ICommand), typeof(SpeedTestView));

    public DownloadSpeed DownloadSpeed
    {
        get => (DownloadSpeed)GetValue(DownloadSpeedProperty);
        set => SetValue(DownloadSpeedProperty, value);
    }

    public bool IsBusy
    {
        get => (bool)GetValue(IsBusyProperty);
        set => SetValue(IsBusyProperty, value);
    }

    public ICommand GetDownloadSpeedCommand
    {
        get => (ICommand)GetValue(GetDownloadSpeedCommandProperty);
        set => SetValue(GetDownloadSpeedCommandProperty, value);
    }

    public SpeedTestView()
		=> InitializeComponent();
}