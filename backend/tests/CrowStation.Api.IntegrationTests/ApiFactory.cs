using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Testcontainers.PostgreSql;
using Testcontainers.Redis;

namespace CrowStation.Api.IntegrationTests;

/// <summary>
/// Boots the real API against throwaway PostgreSQL, Redis and RustFS (S3) containers. Requires Docker.
/// </summary>
public sealed class ApiFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private const string Bucket = "crow-station-tests";
    private const string StorageUser = "crow";
    private const string StoragePassword = "crow_test_password";
    private const int StoragePort = 9000;

    private readonly PostgreSqlContainer _postgres = new PostgreSqlBuilder("postgres:17-alpine").Build();
    private readonly RedisContainer _redis = new RedisBuilder("redis:8-alpine").Build();
    private readonly IContainer _storage = new ContainerBuilder("rustfs/rustfs:1.0.4")
        .WithEnvironment("RUSTFS_ACCESS_KEY", StorageUser)
        .WithEnvironment("RUSTFS_SECRET_KEY", StoragePassword)
        .WithPortBinding(StoragePort, assignRandomHostPort: true)
        .WithWaitStrategy(Wait.ForUnixContainer().UntilHttpRequestIsSucceeded(request => request.ForPort(StoragePort).ForPath("/health")))
        .Build();

    public async ValueTask InitializeAsync()
    {
        await Task.WhenAll(_postgres.StartAsync(), _redis.StartAsync(), _storage.StartAsync());
    }

    public override async ValueTask DisposeAsync()
    {
        await base.DisposeAsync();
        await Task.WhenAll(_postgres.DisposeAsync().AsTask(), _redis.DisposeAsync().AsTask(), _storage.DisposeAsync().AsTask());
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");
        builder.UseSetting("ConnectionStrings:Database", _postgres.GetConnectionString());
        builder.UseSetting("ConnectionStrings:Redis", _redis.GetConnectionString());
        builder.UseSetting("Storage:ServiceUrl", $"http://{_storage.Hostname}:{_storage.GetMappedPublicPort(StoragePort)}");
        builder.UseSetting("Storage:AccessKey", StorageUser);
        builder.UseSetting("Storage:SecretKey", StoragePassword);
        builder.UseSetting("Storage:BucketName", Bucket);
        builder.UseSetting("Storage:CreateBucketIfMissing", "true");
    }
}

[CollectionDefinition(Name)]
public sealed class ApiFixtureGroup : ICollectionFixture<ApiFactory>
{
    public const string Name = "api";
}
