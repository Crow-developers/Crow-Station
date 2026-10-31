using CrowStation.Domain.Common;

namespace CrowStation.Application.Abstractions.Messaging;

/// <summary>
/// A request that reads state and does not change it.
/// </summary>
public interface IQuery<TResponse>;

public interface IQueryHandler<in TQuery, TResponse>
    where TQuery : IQuery<TResponse>
{
    Task<Result<TResponse>> Handle(TQuery query, CancellationToken cancellationToken);
}
