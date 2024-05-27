namespace WiFi_Analyzer;

public partial class AppShell : Shell
{
	public AppShell()
        => InitializeComponent();

    protected override async void OnAppearing()
    {
        if (!Connectivity.Current.NetworkAccess.Equals(NetworkAccess.Internet))
        {
            await Current.DisplayAlert("Network Error", "No internet connection detected.", "Exit");
            Environment.Exit(0);
        }

        base.OnAppearing();
    }
}
