namespace SensorDataCollecting.Client.Enums;

public enum Gender
{
    Male,
    Female,
    Other,
}

public static class GenderExtensions
{
    public static string ToName(this Gender gender) =>
        gender switch
        {
            Gender.Male => "Muž",
            Gender.Female => "Žena",
            Gender.Other => "Jiné",
            _ => gender.ToString()
        };
}
