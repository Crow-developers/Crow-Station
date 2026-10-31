using CrowStation.Api.Infrastructure;
using CrowStation.Application.Abstractions.Messaging;
using CrowStation.Application.Features.Diagnostics;

namespace CrowStation.Api.Endpoints;

internal static class ApiV1
{
    public static IEndpointRouteBuilder MapApiV1(this IEndpointRouteBuilder app)
    {
        var v1 = app.MapGroup("/api/v1");

        v1.MapGet("/diagnostics/time", async (
                IQueryHandler<GetServerTimeQuery, ServerTimeResponse> handler,
                CancellationToken cancellationToken) =>
                (await handler.Handle(new GetServerTimeQuery(), cancellationToken).ConfigureAwait(false)).ToHttpResult())
            .WithName("GetServerTime")
            .WithTags("Diagnostics");

        return app;
    }
}
