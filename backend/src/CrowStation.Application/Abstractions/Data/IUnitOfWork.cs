namespace CrowStation.Application.Abstractions.Data;

/// <summary>
/// Commits all changes made in the current request as one transaction. Implemented by Infrastructure.
/// </summary>
public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
