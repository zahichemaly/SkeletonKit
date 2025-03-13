using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace CME.Common.Extensions
{
    public static class HealthChecksExtensions
    {
        public static IHealthChecksBuilder AddCustomCheckHealth(this IServiceCollection services)
        {
            IHealthChecksBuilder healthChecksBuilder = services
                .AddHealthChecks()
                .AddCheck("self", () => HealthCheckResult.Healthy(), tags: new[] { Constants.HealthCheck.Readiness, Constants.HealthCheck.Liveness });

            return healthChecksBuilder;
        }

        public static IHealthChecksBuilder EnableMongoDb(this IHealthChecksBuilder healthChecksBuilder, string connectionString)
        {
            if (!string.IsNullOrWhiteSpace(connectionString))
            {
                healthChecksBuilder.AddMongoDb(connectionString, tags: new[] { Constants.HealthCheck.Readiness });
            }
            return healthChecksBuilder;
        }

        public static IHealthChecksBuilder EnableRedis(this IHealthChecksBuilder healthChecksBuilder, string connectionString)
        {
            if (!string.IsNullOrWhiteSpace(connectionString))
            {
                healthChecksBuilder.AddRedis(connectionString, tags: new[] { Constants.HealthCheck.Readiness });
            }
            return healthChecksBuilder;
        }

        public static IEndpointConventionBuilder UseLivenessCheckHealth(this IEndpointRouteBuilder endpoints)
        {
            return endpoints.MapHealthChecks("/liveness", new HealthCheckOptions
            {
                Predicate = check => check.Tags.Contains(Constants.HealthCheck.Liveness)
            });
        }

        public static IEndpointConventionBuilder UseReadinessCheckHealth(this IEndpointRouteBuilder endpoints)
        {
            return endpoints.MapHealthChecks("/readiness", new HealthCheckOptions
            {
                Predicate = check => check.Tags.Contains(Constants.HealthCheck.Readiness)
            });
        }
    }
}
