using System.Net;

public sealed class BackendProcessDiscoveryService
{
    private readonly IConfiguration _configuration;

    [ActivatorUtilitiesConstructor]
    public BackendProcessDiscoveryService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GetTtydPath()
    {
        var ttydPath = _configuration["ttydPath"];

        if (string.IsNullOrWhiteSpace(ttydPath) || !File.Exists(ttydPath))
            ttydPath = FindExecutableInPath("ttyd");

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

    public string GetCodePath()
    {
        var codePath = _configuration["codePath"];

        if (string.IsNullOrWhiteSpace(codePath) || !File.Exists(codePath))
            codePath = FindExecutableInPath("code.cmd");

        if (string.IsNullOrWhiteSpace(codePath) || !File.Exists(codePath))
            codePath = TryInferCodeTunnelPath();

        if (string.IsNullOrWhiteSpace(codePath) || !File.Exists(codePath))
            throw new FileNotFoundException("Cannot determine code script path.");

        return codePath;
    }

    private string? TryInferCodeTunnelPath()
        => (Environment.OSVersion.Platform switch
        {
            PlatformID.Win32NT => new string[] {
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Programs", "Microsoft VS Code", "bin", "code-tunnel.exe"),
            },
            _ => new string[] {
                "/usr/bin/code",
            },
        }).FirstOrDefault(x => File.Exists(x));

    public string GetCodeArguments()
    {
        var portValue = 8000; // TODO: Port randomization needed

        var args = new List<string>();
        args.Add("serve-web");
        args.Add("--without-connection-token");
        args.Add("--accept-server-license-terms");
        args.Add("--server-base-path code");

        if (IPEndPoint.MinPort <= portValue && portValue <= IPEndPoint.MaxPort)
            args.Add($"--port {portValue}");

        return string.Join(' ', args);
    }

    private string? FindExecutableInPath(string executableName)
    {
        var pathVariable = Environment.GetEnvironmentVariable("PATH");
        if (string.IsNullOrEmpty(pathVariable))
            return null;

        var paths = pathVariable.Split(Path.PathSeparator);

        foreach (var path in paths)
        {
            var fullPath = Path.Combine(path, executableName);

            // Windows에서는 확장자를 자동으로 추가해서 확인
            if (OperatingSystem.IsWindows())
            {
                var extensions = new[] { "", ".exe", ".cmd", ".bat", ".com" };
                foreach (var ext in extensions)
                {
                    var pathWithExt = fullPath + ext;
                    if (File.Exists(pathWithExt))
                        return pathWithExt;
                }
            }
            else if (File.Exists(fullPath))
            {
                return fullPath;
            }
        }

        return null;
    }
}
