using WiFi_Analyzer.Helpers;
using WiFi_Analyzer.ViewModels;

namespace WiFi_Analyzer.Pages;

public partial class MainPage : ContentPage
{
    readonly MainPageViewModel mainPageViewModel = null!;

    public MainPage()
    {
        mainPageViewModel = ServiceHelper.GetService<MainPageViewModel>()!;

        InitializeComponent();

        BindingContext = mainPageViewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await mainPageViewModel.LoadDataAsync();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        mainPageViewModel.Dispose();
    }
}
