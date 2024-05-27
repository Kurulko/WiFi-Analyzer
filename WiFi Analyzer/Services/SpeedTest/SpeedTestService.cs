using SpeedTest.Net.Enums;
using SpeedTest.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WiFi_Analyzer.Models;

namespace WiFi_Analyzer.Services.SpeedTest;

public class SpeedTestService : ISpeedTestService
{
    public async Task<DownloadSpeed> GetDownloadSpeedAsync()
        => (DownloadSpeed)await FastClient.GetDownloadSpeed(SpeedTestUnit.MegaBitsPerSecond);
}
