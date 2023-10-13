﻿namespace SensorDataCollecting.Client;

public class SensorDataWrapper
{
    public int Id { get; set; }
    public bool IsUploaded { get; set; } = false;
    public DataInfo DataInfo { get; set; }
    public SensorData SensorData { get; set; } = new();
}
