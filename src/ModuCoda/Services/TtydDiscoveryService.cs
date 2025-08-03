using System.Net;

public sealed class TtydDiscoveryService
{
    private readonly IConfiguration _configuration;
    private readonly UtilityService _utilityService;

    [ActivatorUtilitiesConstructor]
    public TtydDiscoveryService(
        IConfiguration configuration,
        UtilityService utilityService)
    {
        _configuration = configuration;
        _utilityService = utilityService;
    }

    public string GetTtydPath()
    {
        var ttydPath = _configuration["ttydPath"];

        if (string.IsNullOrWhiteSpace(ttydPath) || !File.Exists(ttydPath))
            ttydPath = _utilityService.FindExecutableInPath("ttyd");

        if (string.IsNullOrWhiteSpace(ttydPath) || !File.Exists(ttydPath))
            ttydPath = TryInferTtydPath();

        if (string.IsNullOrWhiteSpace(ttydPath) || !File.Exists(ttydPath))
            throw new FileNotFoundException("Cannot determine ttyd binary path.");

        return ttydPath;
    }

    private string? TryInferTtydPath()
        => (Environment.OSVersion.Platform switch
        {
            PlatformID.Win32NT => new string[] {
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Microsoft", "WinGet", "Links", "ttyd.exe"),
            },
            _ => new string[] {
                "/usr/bin/ttyd",
                "/usr/local/bin/ttyd",
                $"/home/{Environment.UserName}/.local/bin/ttyd",
            },
        }).FirstOrDefault(x => File.Exists(x));

    public string GetTtydArguments()
    {
        var credentialValue = _configuration["credential"];
        var interfaceValue = "127.0.0.1";

        var portValue = 7681; // TODO: Port randomization needed

        var args = new List<string>();
        if (!string.IsNullOrWhiteSpace(credentialValue) && credentialValue.Contains(':'))
            args.Add($"--credential {credentialValue}");
        if (!string.IsNullOrWhiteSpace(interfaceValue))
            args.Add($"--interface {interfaceValue}");
        if (IPEndPoint.MinPort <= portValue && portValue <= IPEndPoint.MaxPort)
            args.Add($"--port {portValue}");
        if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            args.Add($"--cwd {Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.Windows))}");
        args.Add("--writable");

        if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            args.Add(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "cmd.exe"));
        else
            args.Add("/bin/bash");

        return string.Join(' ', args);
    }
}
