public interface IProcessManager : IDisposable
{
    event EventHandler<string> OutputReceived;
    event EventHandler<string> ErrorReceived;
    event EventHandler<int> ProcessExited;

    Task<int> StartAsync(string fileName, string arguments = "", string workingDirectory = "", IReadOnlyDictionary<string, string?>? environmentVariables = default, CancellationToken cancellationToken = default);
    Task<int> WaitForExitAsync(CancellationToken cancellationToken = default);
    void Kill();
    bool IsRunning { get; }
    int? ExitCode { get; }
}
