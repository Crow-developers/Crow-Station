using System.ComponentModel.DataAnnotations;

namespace CrowStation.Infrastructure.Storage;

/// <summary>
/// S3-compatible object storage. RustFS locally; the production provider is not decided yet.
/// </summary>
public sealed class StorageOptions
{
    public const string SectionName = "Storage";

    [Required]
    [Url]
    public string ServiceUrl { get; set; } = string.Empty;

    [Required]
    public string AccessKey { get; set; } = string.Empty;

    [Required]
    public string SecretKey { get; set; } = string.Empty;

    [Required]
    public string BucketName { get; set; } = string.Empty;

    public string Region { get; set; } = "us-east-1";

    /// <summary>
    /// Creates the bucket at startup when it is missing. For local development and tests only.
    /// </summary>
    public bool CreateBucketIfMissing { get; set; }
}
