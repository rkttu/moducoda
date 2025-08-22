using Projects;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<ModuCoda>("moducoda")
    .WithHttpEndpoint()
    .WithHttpsEndpoint();

var shell = Environment.OSVersion.Platform switch
{
    PlatformID.Win32NT => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "cmd.exe"),
    _ => "/bin/bash",
};

builder.AddExecutable("ttyd",
    "ttyd",
    string.Empty,
    ["--interface", "127.0.0.1", "--port", "7681", "--cwd", Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "--writable", shell]);

builder.AddExecutable("code-tunnel",
    "code-tunnel",
    string.Empty,
    ["serve-web", "--host", "127.0.0.1", "--port", "8000", "--without-connection-token", "--accept-server-license-terms", "--server-base-path", "vscode"]);

builder.Build().Run();
