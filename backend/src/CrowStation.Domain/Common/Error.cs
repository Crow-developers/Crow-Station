namespace CrowStation.Domain.Common;

/// <summary>
/// A business failure with a stable machine-readable code. Not used for unexpected exceptions.
/// </summary>
public sealed record Error(string Code, string Message, ErrorType Type = ErrorType.Failure)
{
    public static readonly Error None = new(string.Empty, string.Empty);

    public static Error NotFound(string code, string message) => new(code, message, ErrorType.NotFound);

    public static Error Validation(string code, string message) => new(code, message, ErrorType.Validation);

    public static Error Conflict(string code, string message) => new(code, message, ErrorType.Conflict);

    public static Error Forbidden(string code, string message) => new(code, message, ErrorType.Forbidden);
}

public enum ErrorType
{
    Failure,
    Validation,
    NotFound,
    Conflict,
    Forbidden,
}
