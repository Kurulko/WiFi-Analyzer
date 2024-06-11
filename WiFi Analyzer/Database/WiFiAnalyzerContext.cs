using Microsoft.EntityFrameworkCore;
using WiFi_Analyzer.Models;

namespace WiFi_Analyzer.Database;

public class WiFiAnalyzerContext : DbContext
{
    public DbSet<WiFiNetwork> WiFiNetworks { get; set; } = null!;

    public WiFiAnalyzerContext(DbContextOptions<WiFiAnalyzerContext> options) : base(options)
        => Database.EnsureCreated();
}
