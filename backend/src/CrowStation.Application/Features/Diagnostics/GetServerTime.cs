using CrowStation.Application.Abstractions.Messaging;
using CrowStation.Domain.Common;

namespace CrowStation.Application.Features.Diagnostics;

/// <summary>
/// Example use case that shows how a feature flows through the layers. Replace with real features.
/// </summary>
public sealed record GetServerTimeQuery : IQuery<ServerTimeResponse>;

public sealed record ServerTimeResponse(DateTimeOffset UtcNow);

internal sealed class GetServerTimeQueryHandler(TimeProvider timeProvider)
    : IQueryHandler<GetServerTimeQuery, ServerTimeResponse>
{
    public Task<Result<ServerTimeResponse>> Handle(GetServerTimeQuery query, CancellationToken cancellationToken) =>
        Task.FromResult(Result.Success(new ServerTimeResponse(timeProvider.GetUtcNow())));
}
