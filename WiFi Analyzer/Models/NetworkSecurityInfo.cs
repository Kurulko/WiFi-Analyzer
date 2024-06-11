using static NativeWifi.Wlan;

namespace WiFi_Analyzer.Models;

public class NetworkSecurityInfo
{
    public Dot11AuthAlgorithm Authentication { get; set; }
    public Dot11CipherAlgorithm Encryption { get; set; }
}
