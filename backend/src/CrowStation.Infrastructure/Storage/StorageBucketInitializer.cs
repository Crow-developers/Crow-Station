using Amazon.S3;
using Amazon.S3.Util;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CrowStation.Infrastructure.Storage;

/// <summary>
/// Creates the configured bucket on startup when <see cref="StorageOptions.CreateBucketIfMissing"/> is enabled.
/// Production buckets are provisioned outside the application.
/// </summary>
internal sealed partial class StorageBucketInitializer(
    IAmazonS3 s3,
    IOptions<StorageOptions> options,
    ILogger<StorageBucketInitializer> logger) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var settings = options.Value;
        if (!settings.CreateBucketIfMissing)
        {
            return;
        }

        try
        {
            if (!await AmazonS3Util.DoesS3BucketExistV2Async(s3, settings.BucketName).ConfigureAwait(false))
            {
                await s3.PutBucketAsync(settings.BucketName, cancellationToken).ConfigureAwait(false);
                LogBucketCreated(logger, settings.BucketName);
            }
        }
        catch (AmazonS3Exception exception)
        {
            // Startup continues; the readiness check reports storage as unhealthy until it is fixed.
            LogBucketCreationFailed(logger, settings.BucketName, exception);
        }
        catch (HttpRequestException exception)
        {
            LogBucketCreationFailed(logger, settings.BucketName, exception);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    [LoggerMessage(Level = LogLevel.Information, Message = "Created storage bucket {Bucket}")]
    private static partial void LogBucketCreated(ILogger logger, string bucket);

    [LoggerMessage(Level = LogLevel.Warning, Message = "Could not create storage bucket {Bucket}")]
    private static partial void LogBucketCreationFailed(ILogger logger, string bucket, Exception exception);
}
