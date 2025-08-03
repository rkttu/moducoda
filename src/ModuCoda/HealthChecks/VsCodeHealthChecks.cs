using Microsoft.Extensions.Diagnostics.HealthChecks;
using ModuCoda.Services;

namespace ModuCoda.HealthChecks;

public sealed class VsCodeHealthChecks : IHealthCheck
{
    private readonly Configurations _configurations;
    private readonly UtilityService _utilityService;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<VsCodeHealthChecks> _logger;

    [ActivatorUtilitiesConstructor]
    public VsCodeHealthChecks(
        Configurations configurations,
        UtilityService utilityService,
        IHttpClientFactory httpClientFactory,
        ILogger<VsCodeHealthChecks> logger)
    {
        _configurations = configurations;
        _utilityService = utilityService;
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    private DateTime GetLastChecked()
    {
        if (_utilityService.States.TryGetValue("VsCodeHealthCheckLastChecked", out var lastCheckedValue) &&
            lastCheckedValue is DateTime value)
            return value;

        return DateTime.MinValue;
    }

    private DateTime SetLastChecked(DateTime value)
    {
        _utilityService.States["VsCodeHealthCheckLastChecked"] = value;
        return value;
    }

    private HealthCheckResult GetLastStatus()
    {
        if (_utilityService.States.TryGetValue("VsCodeHealthCheckLastStatus", out var lastStatusValue) &&
            lastStatusValue is HealthCheckResult lastStatus)
            return lastStatus;

        return HealthCheckResult.Unhealthy("VsCode health check has not been performed yet.");
    }

    private HealthCheckResult SetLastStatus(HealthCheckResult value)
    {
        _utilityService.States["VsCodeHealthCheckLastStatus"] = value;
        return value;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        var lastChecked = GetLastChecked();
        var lastStatus = GetLastStatus();
        var cached = false;

        try
        {
            if (DateTime.UtcNow - lastChecked < TimeSpan.FromSeconds(10) &&
                lastStatus.Status == HealthStatus.Healthy)
            {
                cached = true;
                return lastStatus;
            }

            var targetUri = _configurations.VsCodeAddress;
            using var client = _httpClientFactory.CreateClient();

            using var requestMessage = new HttpRequestMessage(HttpMethod.Get, targetUri);
            var responseMessage = await client.SendAsync(requestMessage, cancellationToken).ConfigureAwait(false);

            if (!responseMessage.IsSuccessStatusCode)
                return SetLastStatus(HealthCheckResult.Unhealthy("VsCode is not available or running."));

            lastStatus = HealthCheckResult.Healthy("VsCode is available and running.");
            _utilityService.States["VsCodeHealthCheckLastStatus"] = lastStatus;
            return lastStatus;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "VsCode health check failed.");
            return SetLastStatus(HealthCheckResult.Degraded("VsCode health check failed."));
        }
        finally
        {
            if (!cached)
                SetLastChecked(DateTime.UtcNow);
        }
    }
}
