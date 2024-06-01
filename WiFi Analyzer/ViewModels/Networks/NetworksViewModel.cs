using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WiFi_Analyzer.Enums;
using WiFi_Analyzer.Helpers;
using WiFi_Analyzer.Models;
using WiFi_Analyzer.Services;
using WiFi_Analyzer.Services.ConnectedNetwork;
using WiFi_Analyzer.Services.Networks;

namespace WiFi_Analyzer.ViewModels;

public abstract class NetworksViewModel : ViewModelBase
{
    protected readonly INetworksService networksService;

    public IEnumerable<WiFiNetwork> Networks = Enumerable.Empty<WiFiNetwork>();

    protected IEnumerable<WiFiNetwork> filteredWiFiNetworks = Enumerable.Empty<WiFiNetwork>();
    public virtual IEnumerable<WiFiNetwork> FilteredWiFiNetworks
    {
        get => filteredWiFiNetworks;
        set
        {
            filteredWiFiNetworks = value;
            OnPropertyChanged(nameof(FilteredWiFiNetworks));
            OnPropertyChanged(nameof(HasFilteredNetworks));
        }
    }
    public bool HasFilteredNetworks => FilteredWiFiNetworks.Any();

    public ICommand FilterByGHzCommand => new Command<string>(parameter => FilterByGHz(parameter));

    public NetworksViewModel(INetworksService networksService)
        => this.networksService = networksService;

    protected override async Task GetDataAsync()
    {
        await networksService.UpdateWiFiNetworksAsync();
        FilteredWiFiNetworks = Networks = await networksService.GetWiFiNetworksWithStatesAsync();
    }

    protected override async void UpdateStates(object? state = null)
        => FilteredWiFiNetworks = Networks = await networksService.GetWiFiNetworksWithStatesAsync();

    protected void FilterByGHz(string parameter)
        => FilteredWiFiNetworks = NetworksFilter.FilterByGHz(Networks, parameter);
}

