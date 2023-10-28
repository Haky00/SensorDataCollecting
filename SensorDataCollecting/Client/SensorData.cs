using Postgrest.Attributes;
using Postgrest.Models;
using SensorDataCollecting.Client.Enums;

namespace SensorDataCollecting.Client;

public class SensorDataDb : BaseModel
{
    [PrimaryKey(shouldInsert: true)]
    public Guid Id { get; set; }
    public MovementType Movement { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string? Note { get; set; }
    public Gender? Gender { get; set; }
    public string? Phone { get; set; }
    public int? Age { get; set; }
    public int? Weight { get; set; }
    public int? Height { get; set; }
    public string? Accelerometer { get; set; }
    public string? GravitySensor { get; set; }
    public string? Gyroscope { get; set; }
    public string? LinearAccelerationSensor { get; set; }
    //public string? Magnetometer { get; set; }
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
}

public class DataInfo
{
    public Guid Id { get; set; }
    public MovementType Movement { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string? Note { get; set; }
}

public struct SensorXYZ
{
    public double T { get; set; }
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }
}

public struct SensorXYZW
{
    public double T { get; set; }
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }
    public double W { get; set; }
}

