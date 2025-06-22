using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Infrastructure
{
    public static class HealthCheckRegistration
    {
        public static IHealthChecksBuilder AddInfrastructureHealthChecks(
            this IHealthChecksBuilder builder,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            return builder.AddSqlServer(
                connectionString: connectionString,
                failureStatus: HealthStatus.Unhealthy,
                tags: new[] { "ready" });
        }
    }
}
