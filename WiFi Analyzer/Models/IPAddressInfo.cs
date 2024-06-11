namespace WiFi_Analyzer.Models;

public class IPAddressInfo
{
    public string PrivateIPv4 { get; set; } = null!;
    public string SubnetMask { get; set; } = null!;
    public string? PublicIPv4 { get; set; }
}
