using Postgrest.Attributes;
using Postgrest.Models;
using SensorDataCollecting.Client.Enums;
using System.Text.Json;

namespace SensorDataCollecting.Client;

public class SensorDataJson : BaseModel
{
    [PrimaryKey]
    public Guid Id { get; set; }
    public string? AbsoluteOrientationSensor { get; set; }
    public string? Accelerometer { get; set; }
    public string? GravitySensor { get; set; }
    public string? Gyroscope { get; set; }
    public string? LinearAccelerationSensor { get; set; }
    public string? Magnetometer { get; set; }
    public string? RelativeOrientationSensor { get; set; }
}

public class SensorData
{
    public List<GyroscopeData>? Gyroscope { get; set; }

    public SensorDataJson ToJsonData() =>
        new()
        {
            Gyroscope = Gyroscope is null ? null : JsonSerializer.Serialize(Gyroscope)
        };
}

public class DataInfo : BaseModel
{
    [PrimaryKey]
    public Guid Id { get; set; }
    public MovementType Movement { get; set; }
    public int SamplingRate { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}

public struct GyroscopeData
{
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }
}
