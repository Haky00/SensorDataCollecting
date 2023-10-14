using Postgrest.Attributes;
using Postgrest.Models;
using SensorDataCollecting.Client.Enums;
using System.Text.Json;

namespace SensorDataCollecting.Client;

public class SensorDataJson : BaseModel
{
    [PrimaryKey]
    public Guid Id { get; set; }
    public string? Accelerometer { get; set; }
    public string? GravitySensor { get; set; }
    public string? Gyroscope { get; set; }
    public string? LinearAccelerationSensor { get; set; }
    public string? Magnetometer { get; set; }
    public string? AbsoluteOrientationSensor { get; set; }
    public string? RelativeOrientationSensor { get; set; }
}

public class SensorData
{
    public List<SensorXYZ>? Accelerometer { get; set; }
    public List<SensorXYZ>? GravitySensor { get; set; }
    public List<SensorXYZ>? Gyroscope { get; set; }
    public List<SensorXYZ>? LinearAccelerationSensor { get; set; }
    public List<SensorXYZW>? AbsoluteOrientationSensor { get; set; }
    public List<SensorXYZW>? RelativeOrientationSensor { get; set; }

    public SensorDataJson ToJsonData() =>
        new()
        {
            Accelerometer = Accelerometer is null ? null : JsonSerializer.Serialize(Accelerometer),
            GravitySensor = GravitySensor is null ? null : JsonSerializer.Serialize(GravitySensor),
            Gyroscope = Gyroscope is null ? null : JsonSerializer.Serialize(Gyroscope),
            LinearAccelerationSensor = LinearAccelerationSensor is null ? null : JsonSerializer.Serialize(LinearAccelerationSensor),
            AbsoluteOrientationSensor = AbsoluteOrientationSensor is null ? null : JsonSerializer.Serialize(AbsoluteOrientationSensor),
            RelativeOrientationSensor = RelativeOrientationSensor is null ? null : JsonSerializer.Serialize(RelativeOrientationSensor)
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

public struct SensorXYZ
{
    public double Timestamp { get; set; }
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }
}

public struct SensorXYZW
{
    public double Timestamp { get; set; }
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }
    public double W { get; set; }
}

