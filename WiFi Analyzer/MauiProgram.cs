using Microcharts.Maui;
using Microsoft.Extensions.Logging;
using SkiaSharp.Views.Maui.Controls.Hosting;
using WiFi_Analyzer.Helpers;
using WiFi_Analyzer.Services.ConnectedNetwork;
using WiFi_Analyzer.Services.Networks;
using WiFi_Analyzer.Services.SpeedTest;

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


#if DEBUG
        builder.Logging.AddDebug();
#endif

        var app = builder.Build();

        ServiceHelper.Initialize(app.Services);

        return app;
    }
}
