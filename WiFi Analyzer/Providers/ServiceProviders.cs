using Microsoft.Extensions.Configuration;
using WiFi_Analyzer.Services.ConnectedNetwork;
using WiFi_Analyzer.Services.Networks;
using WiFi_Analyzer.Services.SpeedTest;
using WiFi_Analyzer.ViewModels;
using Microsoft.EntityFrameworkCore;
using WiFi_Analyzer.Database;

namespace WiFi_Analyzer.Providers;

public static class ServiceProviders
{
    public static void AddMSSQLServer(this IServiceCollection services)
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        string connection = configuration.GetConnectionString("DefaultConnection")!;
        services.AddDbContext<WiFiAnalyzerContext>(opts =>
        {
            opts.UseSqlServer(connection);
            opts.EnableSensitiveDataLogging();
        });
    }

    public static void AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IConnectedNetworkService, ConnectedNetworkService>();
        services.AddSingleton<ISpeedTestService, SpeedTestService>();
        services.AddSingleton<INetworksService, NetworksService>();
    }

    public static void AddViewModels(this IServiceCollection services)
    {
        services.AddSingleton<ConnectedNetworkViewModel>();
        services.AddSingleton<NetworksTableViewModel>();
        services.AddSingleton<NetworksGraphViewModel>();
        services.AddSingleton<MainPageViewModel>();
    }
}
