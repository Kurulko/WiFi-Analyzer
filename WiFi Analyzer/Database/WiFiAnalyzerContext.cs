using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WiFi_Analyzer.Models;

namespace WiFi_Analyzer.Database;

public class WiFiAnalyzerContext : DbContext
{
    public DbSet<WiFiNetwork> WiFiNetworks { get; set; } = null!;

    public WiFiAnalyzerContext(DbContextOptions<WiFiAnalyzerContext> options) : base(options)
        => Database.EnsureCreated();
}
