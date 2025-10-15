namespace ReisProduction.Wincore.Services;
/// <summary>
/// Disk utility class for retrieving disk information.
/// </summary>
public static class Disk
{
    /// <summary>
    /// Gets all disk information on the system.
    /// </summary>
    public static List<DiskInfo> GetAll()
    {
        List<DiskInfo> disks = [];
        foreach (var drive in DriveInfo.GetDrives())
        {
            if (!drive.IsReady) continue;
            long total = drive.TotalSize / 1_073_741_824;
            long free = drive.TotalFreeSpace / 1_073_741_824;
            disks.Add(new DiskInfo(drive.Name, total, free, drive.DriveFormat, drive.DriveType));
        }
        return disks;
    }
    /// <summary>
    /// Gets disk information by its name (e.g., "C:\"). Case-insensitive.
    /// </summary>
    public static DiskInfo? GetByName(string name) =>
        GetAll().FirstOrDefault(d => d.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    /// <summary>
    /// Gets the largest disk by total size.
    /// </summary>
    public static DiskInfo? GetLargestDisk() =>
        GetAll().OrderByDescending(d => d.TotalGB).FirstOrDefault();
    /// <summary>
    /// Gets the disk with the most free space.
    /// </summary>
    public static DiskInfo? GetLowestFreeDisk() =>
        GetAll().OrderBy(d => d.FreeGB).FirstOrDefault();
    /// <summary>
    /// Gets disks with at least the specified amount of free space in GB.
    /// </summary>
    public static List<DiskInfo> GetByMinimumFreeGB(long minFreeGB) =>
        [.. GetAll().Where(d => d.FreeGB >= minFreeGB)];
    /// <summary>
    /// Gets disks with at least the specified percentage of free space.
    /// </summary>
    public static List<DiskInfo> GetByMinimumPercentFree(double percentFree) =>
        [.. GetAll().Where(d => 100 - d.PercentUsed >= percentFree)];
    /// <summary>
    /// Gets disks that are completely empty (100% free) or completely full (0% free).
    /// </summary>
    public static List<DiskInfo> GetEmptyDisks() =>
        [.. GetAll().Where(d => d.FreeGB == d.TotalGB)];
    /// <summary>
    /// Gets disks that are completely full (0% free).
    /// </summary>
    public static List<DiskInfo> GetFullDisks() =>
        [.. GetAll().Where(d => d.FreeGB is 0)];
}