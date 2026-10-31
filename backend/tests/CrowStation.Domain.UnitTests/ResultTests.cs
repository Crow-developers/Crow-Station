using CrowStation.Domain.Common;

namespace CrowStation.Domain.UnitTests;

public sealed class ResultTests
{
    [Fact]
    public void Success_has_no_error()
    {
        var result = Result.Success();

        result.IsSuccess.Should().BeTrue();
        result.Error.Should().Be(Error.None);
    }

    [Fact]
    public void Failure_carries_the_error()
    {
        var error = Error.NotFound("course.not_found", "The course does not exist.");

        var result = Result.Failure<int>(error);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(error);
    }

    [Fact]
    public void Value_of_a_failure_cannot_be_read()
    {
        var result = Result.Failure<int>(Error.Validation("x", "y"));

        var read = () => result.Value;

        read.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void A_value_converts_implicitly_to_a_successful_result()
    {
        Result<string> result = "hello";

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be("hello");
    }
}
