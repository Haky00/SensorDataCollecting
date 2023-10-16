
using System.Text.Json;

namespace SensorDataCollecting.Client;

public class SensorDataWrapper
{
    public int Id { get; set; }
    public bool IsUploaded { get; set; } = false;
    public DataInfo DataInfo { get; set; } = new();
    public SensorData SensorData { get; set; } = new();
    public SensorDataDb ToDbWrapper(UserInfo info) =>
        new()
        {
            Id = DataInfo.Id,
            StartTime = DataInfo.StartTime,
            EndTime = DataInfo.EndTime,
            Movement = DataInfo.Movement,
            Phone = info.Phone,
            Gender = info.Gender,
            Age = info.Age,
            Height = info.Height,
            Weight = info.Weight,
            Accelerometer = SensorData.Accelerometer is null ? null : JsonSerializer.Serialize(SensorData.Accelerometer),
            GravitySensor = SensorData.GravitySensor is null ? null : JsonSerializer.Serialize(SensorData.GravitySensor),
            Gyroscope = SensorData.Gyroscope is null ? null : JsonSerializer.Serialize(SensorData.Gyroscope),
            LinearAccelerationSensor = SensorData.LinearAccelerationSensor is null ? null : JsonSerializer.Serialize(SensorData.LinearAccelerationSensor),
            //Magnetometer = SensorData.Magnetometer is null ? null : JsonSerializer.Serialize(SensorData.Magnetometer),
            AbsoluteOrientationSensor = SensorData.AbsoluteOrientationSensor is null ? null : JsonSerializer.Serialize(SensorData.AbsoluteOrientationSensor),
            RelativeOrientationSensor = SensorData.RelativeOrientationSensor is null ? null : JsonSerializer.Serialize(SensorData.RelativeOrientationSensor)
        };
}
