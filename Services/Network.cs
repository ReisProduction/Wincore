using static ReisProduction.Wincore.Models.ManagementHelper;
using System.Text.RegularExpressions;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Management;
using System.Net;
namespace ReisProduction.Wincore.Services;
/// <summary>
/// Network-related utility functions.
/// </summary>
public static class Network
{
    /// <summary>
    /// Indicates whether the provided string is a valid IPv4 address.
    /// </summary>
    public static bool IsIPv4(string ip) => IPAddress.TryParse(ip, out var addr) && addr.AddressFamily is AddressFamily.InterNetwork;
    /// <summary>
    /// Indicates whether the provided string is a valid IPv6 address.
    /// </summary>
    public static bool IsIPv6(string ip) => IPAddress.TryParse(ip, out var addr) && addr.AddressFamily is AddressFamily.InterNetworkV6;
    /// <summary>
    /// Gets the MAC addresses of all network interfaces on the system.
    /// </summary>
    public static List<string> GetMacAddresses() =>
        [.. NetworkInterface.GetAllNetworkInterfaces()
        .Select(ni => ni.GetPhysicalAddress().ToString())
        .Where(mac => !string.IsNullOrWhiteSpace(mac))];
    /// <summary>
    /// Gets all active network adapters (those that are currently up).
    /// </summary>
    public static List<NetworkInterface> GetActiveAdapters() =>
        [.. NetworkInterface.GetAllNetworkInterfaces()
        .Where(n => n.OperationalStatus is OperationalStatus.Up)];
    /// <summary>
    /// Gets the DNS server addresses configured on all network interfaces.
    /// </summary>
    public static List<IPAddress> GetDnsServers() =>
        [.. NetworkInterface.GetAllNetworkInterfaces()
        .SelectMany(n => n.GetIPProperties().DnsAddresses)];
    /// <summary>
    /// Gets the gateway addresses configured on all network interfaces.
    /// </summary>
    public static List<IPAddress> GetGateways() =>
        [.. NetworkInterface.GetAllNetworkInterfaces()
        .SelectMany(n => n.GetIPProperties().GatewayAddresses.Select(g => g.Address))
        .Where(a => a is not null)];
    /// <summary>
    /// Pings the specified host (IP address or domain name) and returns true if it is reachable within the given timeout.
    /// </summary>
    public static bool PingHost(string host, int timeoutMs = 3210)
    {
        try
        {
            return new Ping()
                .Send(host, timeoutMs).Status
                is IPStatus.Success;
        }
        catch { return false; }
    }
    /// <summary>
    /// Pings the specified host (IP address or domain name) asynchronously and returns true if it is reachable within the given timeout.
    /// </summary>
    public static async Task<bool> PingHostAsync(string host, int timeoutMs = 3210)
    {
        try
        {
            return (await new Ping()
                .SendPingAsync(host, timeoutMs)).Status
                is IPStatus.Success;
        }
        catch { return false; }
    }
    /// <summary>
    /// Scans the specified subnet (e.g., "192.168.1") for reachable IP addresses by pinging each address in the range 1-254.
    /// </summary>
    public static async Task<List<string>> ScanSubnetAsync(string subnet, int timeoutMs = 500)
    {
        List<string> reachable = [];
        var tasks = Enumerable.Range(1, 254).Select(async i =>
        {
            var ip = $"{subnet}.{i}";
            if (await PingHostAsync(ip, timeoutMs))
                lock (reachable) { reachable.Add(ip); }
        });
        await Task.WhenAll(tasks);
        return reachable;
    }
    /// <summary>
    /// Gets the local IP address of the machine. Set isIPv4 to true for IPv4 address, false for IPv6 address.
    /// </summary>
    public static string GetLocalIP(bool isIPv4)
    {
        try
        {
            return Dns.GetHostEntry(Dns.GetHostName())
                   .AddressList.FirstOrDefault(i => i.AddressFamily ==
                   (isIPv4 ? AddressFamily.InterNetwork : AddressFamily.InterNetworkV6))?.ToString() ?? "IP not found";
        }
        catch (Exception ex) { return $"Error: {ex.Message}"; }
    }
    /// <summary>
    /// Parses the output of the `ipconfig` command and sorts the IP addresses, prioritizing IPv6 over IPv4 and sorting each type in descending order.
    /// </summary>
    public static string ParseAndSort(string ipconfigOutput)
    {
        var matches = Regex.Matches(ipconfigOutput, @"(IPv4|IPv6)[\s\S]*?: ([\da-fA-F\.:]+)")
                           .Cast<Match>().ToList();
        if (matches.Count is 0) return "No IP info found.";
        var ipv6 = matches.Where(m => m.Groups[1].Value == "IPv6")
                          .OrderByDescending(m => m.Groups[2].Value)
                          .Select(m => $"IPv6 : {m.Groups[2].Value}");
        var ipv4 = matches.Where(m => m.Groups[1].Value == "IPv4")
                          .OrderByDescending(m => m.Groups[2].Value)
                          .Select(m => $"IPv4 : {m.Groups[2].Value}");
        return string.Join('\n', ipv6.Concat(ipv4));
    }
    /// <summary>
    /// Gets detailed information about all network interfaces on the system, including their IP addresses, gateways, and DNS servers.
    /// </summary>
    public static string GetIpInformation()
    {
        try
        {
            StringBuilder sb = new();
            var ifs = NetworkInterface.GetAllNetworkInterfaces()
                     .OrderByDescending(n => n.OperationalStatus is OperationalStatus.Up);
            foreach (var ni in ifs)
            {
                var p = ni.GetIPProperties();
                sb.AppendLine($"{ni.Name} — {ni.NetworkInterfaceType} — {ni.OperationalStatus}");
                if (p.UnicastAddresses.Count is not 0)
                    foreach (var ua in p.UnicastAddresses)
                        sb.AppendLine($"  IP: {ua.Address} / Prefix: {ua.PrefixLength}");
                if (p.GatewayAddresses.Count is not 0)
                    foreach (var gw in p.GatewayAddresses)
                        sb.AppendLine($"  Gateway: {gw.Address}");
                if (p.DnsAddresses.Count is not 0)
                    sb.AppendLine($"  DNS: {string.Join(", ", p.DnsAddresses)}");
            }
            return sb.Length is 0 ? "No network interfaces found." : sb.ToString();
        }
        catch (Exception ex) { return $"Error: {ex.Message}"; }
    }
    /// <summary>
    /// Gets all local IP addresses (both IPv4 and IPv6) assigned to the machine.
    /// </summary>
    public static List<IPAddress> GetAllLocalIPs() =>
        [.. NetworkInterface.GetAllNetworkInterfaces()
        .SelectMany(ni => ni.GetIPProperties().UnicastAddresses)
        .Select(ua => ua.Address).Where(a => a.AddressFamily
        is AddressFamily.InterNetwork or AddressFamily.InterNetworkV6)];
    /// <summary>
    /// Indicates whether a QoS policy with the specified name exists.
    /// </summary>
    public static bool IsRestrict(string name)
    {
        try
        {
            ManagementScope scope = new(@"\\.\root\StandardCimv2");
            scope.Connect();
            using ManagementObjectSearcher searcher = new(scope, new($"SELECT Name FROM MSFT_NetQosPolicy WHERE Name = '{name.EscapeWql()}'"));
            using var results = searcher.Get();
            foreach (var _ in results) return true;
            return false;
        }
        catch { return false; }
    }
    /// <summary>
    /// Restricts network bandwidth using a QoS policy with the specified name and throttle rate in bits per second.
    /// </summary>
    public static bool Restrict(string name, ulong throttleBitsPerSecond, bool makeDefault = true)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        try
        {
            ManagementScope scope = new(@"\\.\root\StandardCimv2");
            scope.Connect();
            using ManagementClass mc = new(scope, new("MSFT_NetQosPolicy"), null);
            using var inst = mc.CreateInstance();
            inst["Name"] = name;
            inst["Default"] = makeDefault;
            inst["ThrottleRateActionBitsPerSecond"] = throttleBitsPerSecond;
            inst.Put();
            return true;
        }
        catch { return false; }
    }
    /// <summary>
    /// Unrestricts (removes) the QoS policy with the specified name.
    /// </summary>
    public static bool Unrestrict(string name)
    {
        try
        {
            ManagementScope scope = new(@"\\.\root\StandardCimv2");
            scope.Connect();
            using ManagementObjectSearcher searcher = new(scope, new($"SELECT * FROM MSFT_NetQosPolicy WHERE Name = '{name.EscapeWql()}'"));
            using var results = searcher.Get();
            foreach (var mo in results.Cast<ManagementObject>())
            {
                mo.Delete();
                return true;
            }
            return false;
        }
        catch { return false; }
    }
}