using CrowStation.Domain.Common;

namespace CrowStation.Domain.UnitTests;

public sealed class EntityTests
{
    private sealed class Sample(Guid id) : AggregateRoot<Guid>(id)
    {
        public void Touch() => Raise(new Touched(DateTimeOffset.UnixEpoch));
    }

    private sealed record Touched(DateTimeOffset OccurredAt) : IDomainEvent;

    [Fact]
    public void Entities_with_the_same_id_are_equal()
    {
        var id = Guid.NewGuid();
        var first = new Sample(id);
        var sameId = new Sample(id);
        var otherId = new Sample(Guid.NewGuid());

        (first == sameId).Should().BeTrue();
        first.Should().Be(sameId);
        (first != otherId).Should().BeTrue();
        first.Should().NotBe(otherId);
    }

    [Fact]
    public void Aggregates_collect_and_clear_domain_events()
    {
        var sample = new Sample(Guid.NewGuid());

        sample.Touch();
        sample.DomainEvents.Should().ContainSingle().Which.Should().BeOfType<Touched>();

        sample.ClearDomainEvents();
        sample.DomainEvents.Should().BeEmpty();
    }
}
