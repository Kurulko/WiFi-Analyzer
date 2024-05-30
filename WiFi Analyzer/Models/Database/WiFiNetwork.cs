using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WiFi_Analyzer.Extensions;
using static NativeWifi.Wlan;

namespace WiFi_Analyzer.Models;

[Index(nameof(MacAddress), IsUnique = true)]
public class WiFiNetwork : IEntityBase
{
    public long Id { get; set; }

    [Required]
    public string SSID { get; set; } = null!;

    [Required]
    public string Protocol { get; set; } = null!;
    public DateTime LastSeen { get; set; }

    [Required]
    public byte[] MacAddress { get; set; } = null!;

    [NotMapped]
    public string StringMacAddress => MacAddress.MacAddressToString();

    [Column("Frequency")]
    public long FrequencyInHz { get; set; }
    public int FrequencyInkHz => (int)(FrequencyInHz / Math.Pow(10, 3));
    public int FrequencyInMHz => (int)(FrequencyInHz / Math.Pow(10, 6));
    public int FrequencyInGHz => (int)(FrequencyInHz / Math.Pow(10, 9));

    public int Channel { get; set; }

    [Column("Secured")]
    public bool IsSecured { get; set; }

    [Column("Authentication")]
    public Dot11AuthAlgorithm AuthenticationAlgorithm { get; set; }

    [NotMapped]
    public NetworkStates? NetworkStates { get; set; }

    [NotMapped]
    public IPAddressInfo? IPAddressInfo { get; set; }

    [NotMapped]
    public NetworkSecurityInfo? NetworkSecurityInfo { get; set; }

    [NotMapped]
    public NetworkInfrastructureInfo? NetworkInfrastructureInfo { get; set; }
}
