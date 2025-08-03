using System.Net;

namespace ModuCoda.Services;

public sealed class VsCodeDiscoveryService
{
    private readonly Configurations _configurations;
    private readonly UtilityService _utilityService;

    [ActivatorUtilitiesConstructor]
    public VsCodeDiscoveryService(
        Configurations configurations,
        UtilityService utilityService)
    {
        _configurations = configurations;
        _utilityService = utilityService;
    }

    public string GetCodePath()
    {
        var codePath = _configurations.VsCodePath;

        if (string.IsNullOrWhiteSpace(codePath) || !File.Exists(codePath))
            codePath = _utilityService.FindExecutableInPath("code.cmd");

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

        var args = new List<string>
        {
            "serve-web",
            "--without-connection-token",
            "--accept-server-license-terms",
            "--server-base-path vscode"
        };

        if (IPEndPoint.MinPort <= portValue && portValue <= IPEndPoint.MaxPort)
            args.Add($"--port {portValue}");

        return string.Join(' ', args);
    }
}
