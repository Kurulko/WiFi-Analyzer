using WiFi_Analyzer.Helpers;
using WiFi_Analyzer.ViewModels;

namespace WiFi_Analyzer.Pages.Networks;

public partial class NetworksTablePage : ContentPage
{
    readonly NetworksTableViewModel networksTableViewModel = null!;

    public NetworksTablePage()
    {
        networksTableViewModel = ServiceHelper.GetService<NetworksTableViewModel>()!;

        InitializeComponent();

        BindingContext = networksTableViewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        UpdateNetworks();
    }

    async void UpdateNetworks()
        => await networksTableViewModel.LoadDataAsync();

    async void NavigateToGraphPage(object sender, EventArgs e)
        => await Navigation.PushAsync(new NetworksGraphPage());

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        networksTableViewModel.Dispose();
    }
}