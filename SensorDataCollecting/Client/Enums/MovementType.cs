namespace SensorDataCollecting.Client.Enums;

public enum MovementType
{
    Lay,
    Sit,
    Walk,
    Run,
    StairsDown,
    StairsUp,
    Car,
    Bus,
    Tram,
    Train,
    Metro,
    OnTable,
    Other
}

public static class MovementTypeExtensions
{
    public static string ToName(this MovementType movementType) =>
        movementType switch
        {
            MovementType.Lay => "Ležení",
            MovementType.Sit => "Sezení",
            MovementType.Walk => "Chůze",
            MovementType.Run => "Běh",
            MovementType.StairsDown => "Chůze ze schodů",
            MovementType.StairsUp => "Chůze do schodů",
            MovementType.Car => "Jízda v autě",
            MovementType.Bus => "Jízda v autobusu",
            MovementType.Tram => "Jízda v tramvaji",
            MovementType.Train => "Jízda ve vlaku",
            MovementType.Metro => "Jízda v metru",
            MovementType.OnTable => "Mobil ležící na stole",
            MovementType.Other => "Jiné",
            _ => movementType.ToString()
        };
}
