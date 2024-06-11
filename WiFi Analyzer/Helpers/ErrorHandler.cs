namespace WiFi_Analyzer.Helpers;

public static class ErrorHandler
{
    public static async Task DisplayErrorAsync(string errorMessage)
        => await Shell.Current.DisplayAlert("Error", errorMessage, "OK");
    public static async Task DisplayNetworkErrorAsync(string errorMessage)
        => await Shell.Current.DisplayAlert("Network Error", errorMessage, "Exit");
}
