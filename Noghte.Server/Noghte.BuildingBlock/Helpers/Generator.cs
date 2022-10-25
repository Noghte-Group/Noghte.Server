namespace Noghte.BuildingBlock.Helpers;

public static class Generator
{
    public static string GenerateOTP()
    {
        return new Random().Next(100000, 999999).ToString();
    }
}
