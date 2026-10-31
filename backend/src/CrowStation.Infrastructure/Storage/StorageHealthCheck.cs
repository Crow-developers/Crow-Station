using Amazon.S3;
using Amazon.S3.Util;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;

namespace CrowStation.Infrastructure.Storage;

internal sealed class StorageHealthCheck(IAmazonS3 s3, IOptions<StorageOptions> options) : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        var bucket = options.Value.BucketName;
        try
        {
            var exists = await AmazonS3Util.DoesS3BucketExistV2Async(s3, bucket).ConfigureAwait(false);
            return exists
                ? HealthCheckResult.Healthy($"Bucket '{bucket}' is reachable.")
                : new HealthCheckResult(context.Registration.FailureStatus, $"Bucket '{bucket}' does not exist.");
        }
        catch (AmazonS3Exception exception)
        {
            return new HealthCheckResult(context.Registration.FailureStatus, "Object storage is unreachable.", exception);
        }
        catch (HttpRequestException exception)
        {
            return new HealthCheckResult(context.Registration.FailureStatus, "Object storage is unreachable.", exception);
        }
    }
}
