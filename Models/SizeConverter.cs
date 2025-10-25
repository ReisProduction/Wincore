namespace ReisProduction.Wincore.Models;
/// <summary>
/// Struct providing universal numeric conversion and formatting for data, length, and weight.
/// Supports all numeric types (int, long, float, double, decimal, byte, short).
/// High performance, no nullable types.
/// </summary>
public static class SizeConverter
{
    // ---------------------- Data Size ----------------------
    public static double ToBits<T>(this T bytes) where T : struct, IConvertible => Convert.ToDouble(bytes) * 8d;
    public static double ToBytes<T>(this T bits) where T : struct, IConvertible => Convert.ToDouble(bits) / 8d;
    public static double ToKiloBytes<T>(this T bytes) where T : struct, IConvertible => Convert.ToDouble(bytes) / 1024d;
    public static double ToMegaBytes<T>(this T bytes) where T : struct, IConvertible => Convert.ToDouble(bytes) / 1048576d;
    public static double ToGigaBytes<T>(this T bytes) where T : struct, IConvertible => Convert.ToDouble(bytes) / 1073741824d;
    public static double ToTeraBytes<T>(this T bytes) where T : struct, IConvertible => Convert.ToDouble(bytes) / 1099511627776d;
    public static string FormatDataSize<T>(this T bytes, int precision = 2, string prefix = "", string suffix = "") where T : struct, IConvertible
    {
        string[] units = ["B", "KB", "MB", "GB", "TB", "PB"];
        double size = Convert.ToDouble(bytes);
        int order = 0;
        while (size >= 1024 && order < units.Length - 1)
        {
            order++;
            size /= 1024;
        }
        return $"{prefix}{Math.Round(size, precision)} {units[order]}{suffix}";
    }
    // ---------------------- Data Rate ----------------------
    public static double ToKilobits<T>(this T bytes) where T : struct, IConvertible => Convert.ToDouble(bytes) * 8d / 1024d;
    public static double ToMegabits<T>(this T bytes) where T : struct, IConvertible => Convert.ToDouble(bytes) * 8d / 1048576d;
    public static double ToGigabits<T>(this T bytes) where T : struct, IConvertible => Convert.ToDouble(bytes) * 8d / 1073741824d;
    public static string FormatDataRate<T>(this T bytes, int precision = 2, string prefix = "", string suffix = "") where T : struct, IConvertible
    {
        string[] units = ["bps", "Kbps", "Mbps", "Gbps"];
        double size = Convert.ToDouble(bytes) * 8d;
        int order = 0;
        while (size >= 1024 && order < units.Length - 1)
        {
            order++;
            size /= 1024;
        }
        return $"{prefix}{Math.Round(size, precision)} {units[order]}{suffix}";
    }

    // ---------------------- Length ----------------------
    public static double ToMillimeters<T>(this T meters) where T : struct, IConvertible => Convert.ToDouble(meters) * 1000d;
    public static double ToCentimeters<T>(this T meters) where T : struct, IConvertible => Convert.ToDouble(meters) * 100d;
    public static double ToMeters<T>(this T centimeters) where T : struct, IConvertible => Convert.ToDouble(centimeters) / 100d;
    public static double ToKilometers<T>(this T meters) where T : struct, IConvertible => Convert.ToDouble(meters) / 1000d;
    public static double ToMetersFromKilometers<T>(this T km) where T : struct, IConvertible => Convert.ToDouble(km) * 1000d;
    public static string FormatLength<T>(this T meters, int precision = 2, string prefix = "", string suffix = "") where T : struct, IConvertible
    {
        string[] units = ["mm", "cm", "m", "km"];
        double size = Convert.ToDouble(meters);
        int order = 2;
        if (size < 1)
        {
            size *= 1000;
            order = 0;
            if (size >= 10)
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
    public static double ToGrams<T>(this T kilograms) where T : struct, IConvertible => Convert.ToDouble(kilograms) * 1000d;
    public static double ToKilograms<T>(this T grams) where T : struct, IConvertible => Convert.ToDouble(grams) / 1000d;
    public static double ToTonnes<T>(this T kilograms) where T : struct, IConvertible => Convert.ToDouble(kilograms) / 1000d;
    public static double ToKilogramsFromTonnes<T>(this T tonnes) where T : struct, IConvertible => Convert.ToDouble(tonnes) * 1000d;
    public static string FormatWeight<T>(this T grams, int precision = 2, string prefix = "", string suffix = "") where T : struct, IConvertible
    {
        string[] units = ["g", "kg", "t"];
        double size = Convert.ToDouble(grams);
        int order = 0;
        while (size >= 1000 && order < units.Length - 1)
        {
            order++;
            size /= 1000;
        }
        return $"{prefix}{Math.Round(size, precision)} {units[order]}{suffix}";
    }
}