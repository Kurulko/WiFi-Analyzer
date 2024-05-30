using Microcharts.Maui;
using Microsoft.Extensions.Logging;
using SkiaSharp.Views.Maui.Controls.Hosting;
using WiFi_Analyzer.Helpers;
using WiFi_Analyzer.Services.ConnectedNetwork;
using WiFi_Analyzer.Services.Networks;
using WiFi_Analyzer.Services.SpeedTest;
using WiFi_Analyzer.ViewModels;
using WiFi_Analyzer.Providers;
using Microsoft.Extensions.Configuration;

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

        services.AddMSSQLServer();
        services.AddServices();
        services.AddViewModels();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        MauiApp app = builder.Build();

        ServiceHelper.Initialize(app.Services);

        return app;
    }
}
