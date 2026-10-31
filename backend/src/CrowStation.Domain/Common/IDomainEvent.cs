namespace CrowStation.Domain.Common;

/// <summary>
/// Something meaningful that happened in the domain. Raised by aggregates and dispatched after persistence.
/// </summary>
public interface IDomainEvent
{
    DateTimeOffset OccurredAt { get; }
}
