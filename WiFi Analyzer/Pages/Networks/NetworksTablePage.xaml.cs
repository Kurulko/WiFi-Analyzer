using System.Windows.Input;
using WiFi_Analyzer.Enums;
using WiFi_Analyzer.Models;
using WiFi_Analyzer.Services.Networks;
using System.Collections.Generic;
using System.Linq;
using WiFi_Analyzer.Extensions;
using WiFi_Analyzer.Helpers;
using WiFi_Analyzer.Services.SpeedTest;

namespace WiFi_Analyzer.Pages.Networks;

public partial class NetworksTablePage : ContentPage
{
    readonly INetworksService networksService;

    public ICommand SortCommand => new Command<string>(propertyName => SortBy(propertyName));
    public ICommand FilterByGHzCommand => new Command<string>(parameter => FilterByGHz(parameter));

    IEnumerable<WiFiNetwork> wifiNetworks = null!;
    public IEnumerable<WiFiNetwork> FilteredWiFiNetworks { get; set; } = null!;

    public bool HasNetworks => FilteredWiFiNetworks.Any();

    public Dictionary<string, OrderBy> SortDirections { get; set; } = new ();

    public NetworksTablePage()
    {
        networksService = ServiceHelper.GetService<INetworksService>()!;

        InitializeComponent();

        GetNetworksDetails();

        BindingContext = this;
    }

    void SortBy(string propertyName)
    {
        OrderBy currentDirection = SortDirections.ContainsKey(propertyName) ? SortDirections[propertyName] : OrderBy.Ascending;

        OrderBy newDirection = ChangeOrderBy(currentDirection);

        SortDirections = new Dictionary<string, OrderBy>();
        SortDirections[propertyName] = newDirection;

        FilteredWiFiNetworks = wifiNetworks.AsQueryable().OrderBy(propertyName, newDirection).ToList();

        OnPropertyChanged(nameof(FilteredWiFiNetworks));
        OnPropertyChanged(nameof(SortDirections));
    }


    void FilterByGHz(string parameter)
    {
        FilteredWiFiNetworks = WiFiNetworksFilter.FilterByGHz(wifiNetworks, parameter);

        OnPropertyChanged(nameof(HasNetworks));
        OnPropertyChanged(nameof(FilteredWiFiNetworks));
    }


    OrderBy ChangeOrderBy(OrderBy orderBy)
        => orderBy == OrderBy.Ascending ? OrderBy.Descending : OrderBy.Ascending;

    async void GetNetworksDetails()
    {
        try
        {
            wifiNetworks = networksService.GetWiFiNetworks();
            FilteredWiFiNetworks = wifiNetworks.ToList();
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
        }
    }

    void UpdateWiFiNetworks(object sender, EventArgs e)
    {
        GetNetworksDetails();

        OnPropertyChanged(nameof(HasNetworks));
        OnPropertyChanged(nameof(FilteredWiFiNetworks));
    }

    async void NavigateToGraphPage(object sender, EventArgs e)
        => await Navigation.PushAsync(new NetworksGraphPage());
}