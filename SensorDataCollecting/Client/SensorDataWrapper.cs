using SensorDataCollecting.Shared;

namespace SensorDataCollecting.Client;

public class SensorDataWrapper
{
    public int Id { get; set; }
    public bool IsUploaded { get; set; } = false;
    public SensorData SensorData { get; set; } = new();
}
