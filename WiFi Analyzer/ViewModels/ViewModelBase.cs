using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WiFi_Analyzer.Helpers;
using WiFi_Analyzer.Models;
using WiFi_Analyzer.Services.ConnectedNetwork;

namespace WiFi_Analyzer.ViewModels;

public abstract class ViewModelBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    public ICommand LoadDataCommand => new Command(async () =>
    {
        try
        {
            await LoadDataAsync();
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
        }
    });

    public async Task LoadDataAsync()
    {
        try
        {
            await GetDataAsync();
        }
        catch (Exception ex)
        {
            await ErrorHandler.DisplayErrorAsync(ex.Message);
        }
    }

    protected abstract Task GetDataAsync();

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}