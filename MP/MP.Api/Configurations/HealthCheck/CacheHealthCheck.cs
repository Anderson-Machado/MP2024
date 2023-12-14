using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace MP.Api.Configurations.HealthCheck
{
    public class CacheHealthCheck : IHealthCheck
    {
        private readonly IDistributedCache _cache;

        public CacheHealthCheck(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            // Sets health check name based on cache implementation (memory cache, redis, etc.)
            context.Registration.Name = _cache.GetType().Name.ToLower();

            try
            {
                // Sends a meaningless entry for the cache. If no exception is raised, it reports as healthy.
                var entryOptions = new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMilliseconds(1) };
                await _cache.SetStringAsync("healthCheckPing", "", entryOptions, cancellationToken);

                return HealthCheckResult.Healthy();
            }
            catch (Exception ex)
            {
                return new HealthCheckResult(context.Registration.FailureStatus, exception: ex);
            }
        }
    }
}