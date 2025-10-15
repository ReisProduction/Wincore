namespace ReisProduction.Wincore.Models;
/// <summary>
/// Static class providing methods to convert and format sizes for data, length, and weight.
/// </summary>
public static class SizeConverter
{
    // ---------------------- Data Size ----------------------
    public static double ToBits(this long bytes) => bytes * 8d;
    public static double ToBytes(this long bits) => bits / 8d;
    public static double ToKiloBytes(this long bytes) => bytes / 1024d;
    public static double ToMegaBytes(this long bytes) => bytes / 1048576d;
    public static double ToGigaBytes(this long bytes) => bytes / 1073741824d;
    public static double ToTeraBytes(this long bytes) => bytes / 1099511627776d;
    public static string FormatDataSize(this long bytes, int precision = 2, string prefix = "", string suffix = "")
    {
        string[] units = ["B", "KB", "MB", "GB", "TB", "PB"];
        double size = bytes;
        int order = 0;
        while (size >= 1024 && order < units.Length - 1)
        {
            order++;
            size /= 1024;
        }
        return $"{prefix}{Math.Round(size, precision)} {units[order]}{suffix}";
    }
    // ---------------------------- Data Rate ----------------------------
    public static double ToKilobits(this long bytes) => bytes * 8d / 1024d;
    public static double ToMegabits(this long bytes) => bytes * 8d / 1048576d;
    public static double ToGigabits(this long bytes) => bytes * 8d / 1073741824d;
    public static string FormatDataRate(this long bytes, int precision = 2, string prefix = "", string suffix = "")
    {
        string[] units = ["bps", "Kbps", "Mbps", "Gbps"];
        double size = bytes * 8d;
        int order = 0;
        while (size >= 1024 && order < units.Length - 1)
        {
            order++;
            size /= 1024;
        }
        return $"{prefix}{Math.Round(size, precision)} {units[order]}{suffix}";
    }
    // ---------------------- Length ----------------------
    public static double ToMillimeters(this double meters) => meters * 1000d;
    public static double ToCentimeters(this double meters) => meters * 100d;
    public static double ToMeters(this double centimeters) => centimeters / 100d;
    public static double ToKilometers(this double meters) => meters / 1000d;
    public static double ToMetersFromKilometers(this double km) => km * 1000d;
    public static string FormatLength(this double meters, int precision = 2, string prefix = "", string suffix = "")
    {
        string[] units = ["mm", "cm", "m", "km"];
        double size = meters;
        int order = 2; // start with meters as default
        if (size < 1)
        {
            size *= 1000; // convert to mm
            order = 0;
            if (size >= 10) // if bigger than 10 mm, use cm
            {
                size /= 10;
                order = 1;
            }
        }
        else if (size >= 1000)
        {
            size /= 1000;
            order = 3;
        }
        return $"{prefix}{Math.Round(size, precision)} {units[order]}{suffix}";
    }
    // ---------------------- Weight ----------------------
    public static double ToGrams(this double kilograms) => kilograms * 1000d;
    public static double ToKilograms(this double grams) => grams / 1000d;
    public static double ToTonnes(this double kilograms) => kilograms / 1000d;
    public static double ToKilogramsFromTonnes(this double tonnes) => tonnes * 1000d;
    public static string FormatWeight(this double grams, int precision = 2, string prefix = "", string suffix = "")
    {
        string[] units = ["g", "kg", "t"];
        double size = grams;
        int order = 0;
        while (size >= 1000 && order < units.Length - 1)
        {
            order++;
            size /= 1000;
        }
        return $"{prefix}{Math.Round(size, precision)} {units[order]}{suffix}";
    }
}