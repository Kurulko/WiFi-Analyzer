using WiFi_Analyzer.Helpers;

namespace WiFi_Analyzer;

public partial class AppShell : Shell
{
	public AppShell()
        => InitializeComponent();

    protected override async void OnAppearing()
    {
        if (!Connectivity.Current.NetworkAccess.Equals(NetworkAccess.Internet))
        {
            await ErrorHandler.DisplayNetworkErrorAsync("No internet connection detected.");
            Environment.Exit(0);
        }

        base.OnAppearing();
    }
}
