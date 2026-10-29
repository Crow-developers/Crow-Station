using Microsoft.Extensions.Diagnostics.HealthChecks;
using StackExchange.Redis;

namespace CrowStation.Infrastructure.Caching;

internal sealed class RedisHealthCheck(IConnectionMultiplexer redis) : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            var latency = await redis.GetDatabase().PingAsync().ConfigureAwait(false);
            return HealthCheckResult.Healthy($"Ping {latency.TotalMilliseconds:F0} ms");
        }
        catch (RedisException exception)
        {
            return new HealthCheckResult(context.Registration.FailureStatus, "Redis is unreachable.", exception);
        }
    }
}
