using SensorDataCollecting.Shared.Enums;

namespace SensorDataCollecting.Shared;

public class SensorData
{
    public Guid Guid { get; set; }
    public MovementType Movement { get; set; }
    public int SamplingRate { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public List<GyroscopeData>? Gyroscope { get; set; }
}

public class GyroscopeData
{
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }
}
