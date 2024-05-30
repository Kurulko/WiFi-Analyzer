using SpeedTest.Net.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WiFi_Analyzer.Extensions;

namespace WiFi_Analyzer.Models;

public class DownloadSpeed
{
    public double Speed { get; set; }
    public SpeedTestUnit Unit { get; set; }

    public static explicit operator DownloadSpeed(SpeedTest.Net.Models.DownloadSpeed _downloadSpeed)
    {
        DownloadSpeed downloadSpeed = new();

        downloadSpeed.Speed = _downloadSpeed.Speed;
        downloadSpeed.Unit = _downloadSpeed.Unit.ParseToSpeedTestUnit();

        return downloadSpeed;
    }
}
