using WiFi_Analyzer.Pages;

namespace WiFi_Analyzer;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();
    }
}
