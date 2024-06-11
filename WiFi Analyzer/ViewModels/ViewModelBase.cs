using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WiFi_Analyzer.Helpers;

namespace WiFi_Analyzer.ViewModels;

public abstract class ViewModelBase : INotifyPropertyChanged, IDisposable
{
    Timer? networkStateTimer;

    public ViewModelBase()
        => networkStateTimer = new Timer(UpdateStates, null, TimeSpan.Zero, TimeSpan.FromSeconds(30));

    protected abstract void UpdateStates(object? _ = null);

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

    public void Dispose()
        => networkStateTimer?.Dispose();
}