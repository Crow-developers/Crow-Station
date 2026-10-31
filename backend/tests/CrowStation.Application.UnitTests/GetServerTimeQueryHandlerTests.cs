using CrowStation.Application.Abstractions.Messaging;
using CrowStation.Application.Features.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;

namespace CrowStation.Application.UnitTests;

public sealed class GetServerTimeQueryHandlerTests
{
    [Fact]
    public async Task Returns_the_current_utc_time()
    {
        var now = new DateTimeOffset(2026, 10, 29, 12, 0, 0, TimeSpan.Zero);
        var clock = Substitute.For<TimeProvider>();
        clock.GetUtcNow().Returns(now);

        var result = await new GetServerTimeQueryHandler(clock).Handle(new GetServerTimeQuery(), TestContext.Current.CancellationToken);

        result.IsSuccess.Should().BeTrue();
        result.Value.UtcNow.Should().Be(now);
    }

    [Fact]
    public void AddApplication_registers_query_handlers()
    {
        using var provider = new ServiceCollection().AddApplication().BuildServiceProvider();
        using var scope = provider.CreateScope();

        scope.ServiceProvider.GetService<IQueryHandler<GetServerTimeQuery, ServerTimeResponse>>()
            .Should().BeOfType<GetServerTimeQueryHandler>();
    }
}
