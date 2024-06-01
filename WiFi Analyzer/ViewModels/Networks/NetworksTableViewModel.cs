using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WiFi_Analyzer.Enums;
using WiFi_Analyzer.Services.Networks;
using WiFi_Analyzer.Extensions;

namespace WiFi_Analyzer.ViewModels;

public class NetworksTableViewModel : NetworksViewModel
{
    public ICommand SortCommand => new Command<string>(propertyName => SortBy(propertyName));

    public NetworksTableViewModel(INetworksService networksService) : base(networksService)
    {
    }

    public Dictionary<string, OrderBy> SortDirections { get; set; } = new();
    void SortBy(string propertyName)
    {
        OrderBy currentDirection = SortDirections.ContainsKey(propertyName) ? SortDirections[propertyName] : OrderBy.Ascending;

        OrderBy newDirection = ChangeOrderBy(currentDirection);

        SortDirections = new Dictionary<string, OrderBy>();
        SortDirections[propertyName] = newDirection;

        FilteredWiFiNetworks = FilteredWiFiNetworks.AsQueryable().DynamicOrderBy(propertyName, newDirection).ToList();

        OnPropertyChanged(nameof(SortDirections));
    }

    OrderBy ChangeOrderBy(OrderBy orderBy)
        => orderBy == OrderBy.Ascending ? OrderBy.Descending : OrderBy.Ascending;
}
