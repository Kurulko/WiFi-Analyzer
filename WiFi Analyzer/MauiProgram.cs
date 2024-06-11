using Microcharts.Maui;
using Microsoft.Extensions.Logging;
using WiFi_Analyzer.Helpers;
using WiFi_Analyzer.Providers;

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
