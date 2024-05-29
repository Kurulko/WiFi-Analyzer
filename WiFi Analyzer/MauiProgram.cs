using Microcharts.Maui;
using Microsoft.Extensions.Logging;
using SkiaSharp.Views.Maui.Controls.Hosting;
using WiFi_Analyzer.Helpers;
using WiFi_Analyzer.Services.ConnectedNetwork;
using WiFi_Analyzer.Services.Networks;
using WiFi_Analyzer.Services.SpeedTest;
using WiFi_Analyzer.ViewModels;

namespace WiFi_Analyzer;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		MauiAppBuilder builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .UseMicrocharts()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        IServiceCollection services = builder.Services;

        services.AddSingleton<IConnectedNetworkService, ConnectedNetworkService>();
        services.AddSingleton<ISpeedTestService, SpeedTestService>();
        services.AddSingleton<INetworksService, NetworksService>();

        services.AddSingleton<ConnectedNetworkViewModel>();
        services.AddSingleton<NetworksTableViewModel>();
        services.AddSingleton<NetworksGraphViewModel>();
        services.AddSingleton<MainPageViewModel>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        MauiApp app = builder.Build();

        ServiceHelper.Initialize(app.Services);

        return app;
    }
}
