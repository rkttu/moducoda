public sealed class BackendProcessManager : BackgroundService
{
    private ILogger<BackendProcessManager> _logger;
    private TtydDiscoveryService _ttydDiscoveryService;
    private VsCodeDiscoveryService _vsCodeDiscoveryService;
    private IProcessManagerFactory _processManagerFactory;

    [ActivatorUtilitiesConstructor]
    public BackendProcessManager(
        ILogger<BackendProcessManager> logger,
        TtydDiscoveryService discoveryService,
        VsCodeDiscoveryService vsCodeDiscoveryService,
        IProcessManagerFactory processManagerFactory)
    {
        _logger = logger;
        _ttydDiscoveryService = discoveryService;
        _vsCodeDiscoveryService = vsCodeDiscoveryService;
        _processManagerFactory = processManagerFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var ttydProcess = _processManagerFactory.Create();
        using var codeProcess = _processManagerFactory.Create();

        try
        {
            stoppingToken.Register(() =>
            {
                ttydProcess.Kill();
                codeProcess.Kill();
            });

            var ttydTask = ttydProcess.StartAsync(
                _ttydDiscoveryService.GetTtydPath(),
                _ttydDiscoveryService.GetTtydArguments(),
                cancellationToken: stoppingToken);
            var codeTask = codeProcess.StartAsync(
                _vsCodeDiscoveryService.GetCodePath(),
                _vsCodeDiscoveryService.GetCodeArguments(),
                environmentVariables: new Dictionary<string, string?>
                {
                    { "DONT_PROMPT_WSL_INSTALL", string.Empty },
                },
                cancellationToken: stoppingToken);
            await Task.WhenAll(ttydTask, codeTask).ConfigureAwait(false);

            var ttydWaitTask = ttydProcess.WaitForExitAsync(stoppingToken);
            var codeWaitTask = codeProcess.WaitForExitAsync(stoppingToken);
            await Task.WhenAll(ttydWaitTask, codeWaitTask).ConfigureAwait(false);
            return;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error occurred.");
        }
        finally
        {
            ttydProcess.Kill();
            codeProcess.Kill();
        }
    }
}
