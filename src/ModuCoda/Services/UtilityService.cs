using System.Collections.Concurrent;

namespace ModuCoda.Services;

public sealed class UtilityService
{
    private readonly ConcurrentDictionary<string, object?> _states = new ConcurrentDictionary<string, object?>();

    public IDictionary<string, object?> States => _states;

    public string? FindExecutableInPath(string executableName)
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
