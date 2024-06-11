using WiFi_Analyzer.Models;

namespace WiFi_Analyzer.Services.SpeedTest;

public interface ISpeedTestService
{
    Task<DownloadSpeed> GetDownloadSpeedAsync();
}
