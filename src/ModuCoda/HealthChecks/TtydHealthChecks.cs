using Microsoft.Extensions.Diagnostics.HealthChecks;
using ModuCoda.Services;

namespace ModuCoda.HealthChecks;

public sealed class TtydHealthChecks : IHealthCheck
{
    private readonly Configurations _configurations;
    private readonly UtilityService _utilityService;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<TtydHealthChecks> _logger;

    [ActivatorUtilitiesConstructor]
    public TtydHealthChecks(
        Configurations configurations,
        UtilityService utilityService,
        IHttpClientFactory httpClientFactory,
        ILogger<TtydHealthChecks> logger)
    {
        _configurations = configurations;
        _utilityService = utilityService;
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    private DateTime GetLastChecked()
    {
        if (_utilityService.States.TryGetValue("TtydHealthCheckLastChecked", out var lastCheckedValue) &&
            lastCheckedValue is DateTime value)
            return value;

        return DateTime.MinValue;
    }

    private DateTime SetLastChecked(DateTime value)
    {
        _utilityService.States["TtydHealthCheckLastChecked"] = value;
        return value;
    }

    private HealthCheckResult GetLastStatus()
    {
        if (_utilityService.States.TryGetValue("TtydHealthCheckLastStatus", out var lastStatusValue) &&
            lastStatusValue is HealthCheckResult lastStatus)
            return lastStatus;

        return HealthCheckResult.Unhealthy("Ttyd health check has not been performed yet.");
    }

    private HealthCheckResult SetLastStatus(HealthCheckResult value)
    {
        _utilityService.States["TtydHealthCheckLastStatus"] = value;
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

            var targetUri = _configurations.TtydAddress;
            using var client = _httpClientFactory.CreateClient();

            using var requestMessage = new HttpRequestMessage(HttpMethod.Head, targetUri);
            var responseMessage = await client.SendAsync(requestMessage, cancellationToken).ConfigureAwait(false);

            if (!responseMessage.IsSuccessStatusCode)
                return SetLastStatus(HealthCheckResult.Unhealthy("Ttyd is not available or running."));

            var name = responseMessage.Headers.Server.FirstOrDefault()?.Product?.Name;
            if (name == null || name.StartsWith("ttyd/", StringComparison.Ordinal))
                return SetLastStatus(HealthCheckResult.Unhealthy("Ttyd is not available or running."));

            lastStatus = HealthCheckResult.Healthy("Ttyd is available and running.");
            _utilityService.States["TtydHealthCheckLastStatus"] = lastStatus;
            return lastStatus;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ttyd health check failed.");
            return SetLastStatus(HealthCheckResult.Degraded("Ttyd health check failed."));
        }
        finally
        {
            if (!cached)
                SetLastChecked(DateTime.UtcNow);
        }
    }
}
