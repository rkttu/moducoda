using Microsoft.Extensions.Diagnostics.HealthChecks;
using ModuCoda.Services;

namespace ModuCoda.HealthChecks;

public sealed class TtydHealthChecks : IHealthCheck
{
    private readonly Configurations _configurations;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<TtydHealthChecks> _logger;

    [ActivatorUtilitiesConstructor]
    public TtydHealthChecks(
        Configurations configurations,
        IHttpClientFactory httpClientFactory,
        ILogger<TtydHealthChecks> logger)
    {
        _configurations = configurations;
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    private DateTime _lastChecked;
    private HealthCheckResult _lastStatus = HealthCheckResult.Unhealthy();

    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            if (DateTime.UtcNow - _lastChecked < TimeSpan.FromSeconds(10) &&
                _lastStatus.Status == HealthStatus.Healthy)
                return HealthCheckResult.Healthy("Ttyd is available and running.");

            var targetUri = _configurations.TtydAddress;
            using var client = _httpClientFactory.CreateClient();

            using var requestMessage = new HttpRequestMessage(HttpMethod.Head, targetUri);
            var responseMessage = await client.SendAsync(requestMessage, cancellationToken).ConfigureAwait(false);

            if (!responseMessage.IsSuccessStatusCode)
                return _lastStatus = HealthCheckResult.Unhealthy("Ttyd is not available or running.");

            var name = responseMessage.Headers.Server.FirstOrDefault()?.Product?.Name;
            if (name == null || name.StartsWith("ttyd/", StringComparison.Ordinal))
                return _lastStatus = HealthCheckResult.Unhealthy("Ttyd is not available or running.");

            return _lastStatus = HealthCheckResult.Healthy("Ttyd is available and running.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ttyd health check failed.");
            return _lastStatus = HealthCheckResult.Degraded("Ttyd health check failed.");
        }
        finally
        {
            _lastChecked = DateTime.UtcNow;
        }
    }
}
