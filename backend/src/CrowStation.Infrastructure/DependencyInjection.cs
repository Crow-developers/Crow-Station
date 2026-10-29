using Amazon.Runtime;
using Amazon.S3;
using CrowStation.Application.Abstractions.Data;
using CrowStation.Infrastructure.Caching;
using CrowStation.Infrastructure.Persistence;
using CrowStation.Infrastructure.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace CrowStation.Infrastructure;

public static class DependencyInjection
{
    /// <summary>
    /// Tag for health checks that must pass before the service receives traffic.
    /// </summary>
    public const string ReadinessTag = "ready";

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);

        return services
            .AddPersistence(configuration)
            .AddCaching(configuration)
            .AddStorage(configuration);
    }

    private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = RequiredConnectionString(configuration, "Database");

        services.AddDbContext<AppDbContext>(options => options.UseNpgsql(
            connectionString,
            npgsql => npgsql.MigrationsHistoryTable("__ef_migrations_history", AppDbContext.Schema)));

        services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<AppDbContext>());

        services.AddHealthChecks().AddDbContextCheck<AppDbContext>("database", tags: [ReadinessTag]);

        return services;
    }

    private static IServiceCollection AddCaching(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = RequiredConnectionString(configuration, "Redis");

        services.AddSingleton<IConnectionMultiplexer>(_ =>
        {
            var options = ConfigurationOptions.Parse(connectionString);
            options.AbortOnConnectFail = false;
            return ConnectionMultiplexer.Connect(options);
        });

        services.AddHealthChecks().AddCheck<RedisHealthCheck>("cache", tags: [ReadinessTag]);

        return services;
    }

    private static IServiceCollection AddStorage(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<StorageOptions>()
            .Bind(configuration.GetSection(StorageOptions.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddSingleton<IAmazonS3>(provider =>
        {
            var options = provider.GetRequiredService<IOptions<StorageOptions>>().Value;
            var config = new AmazonS3Config
            {
                ServiceURL = options.ServiceUrl,
                AuthenticationRegion = options.Region,
                ForcePathStyle = true,
            };
            return new AmazonS3Client(new BasicAWSCredentials(options.AccessKey, options.SecretKey), config);
        });

        services.AddHostedService<StorageBucketInitializer>();
        services.AddHealthChecks().AddCheck<StorageHealthCheck>("storage", tags: [ReadinessTag]);

        return services;
    }

    private static string RequiredConnectionString(IConfiguration configuration, string name) =>
        configuration.GetConnectionString(name) is { Length: > 0 } value
            ? value
            : throw new InvalidOperationException($"Connection string '{name}' is not configured.");
}
