using SensorDataCollecting.Client.Enums;

namespace SensorDataCollecting.Client;

public class UserInfo
{
    public Gender? Gender { get; set; }
    public string? Phone { get; set; }
    public int? Age { get; set; }
    public int? Height {  get; set; }
    public int? Weight { get; set; }
}
