using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WiFi_Analyzer.Models;

namespace WiFi_Analyzer.Services.SpeedTest;

public interface ISpeedTestService
{
    Task<DownloadSpeed> GetDownloadSpeedAsync();
}
