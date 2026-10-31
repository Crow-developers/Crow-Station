using System.Net;
using System.Net.Http.Json;

namespace CrowStation.Api.IntegrationTests;

[Collection(ApiFixtureGroup.Name)]
public sealed class ApiTests(ApiFactory factory)
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task Liveness_is_healthy()
    {
        var response = await _client.GetAsync(new Uri("/health/live", UriKind.Relative), TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Readiness_reports_database_cache_and_storage_as_healthy()
    {
        var response = await _client.GetAsync(new Uri("/health/ready", UriKind.Relative), TestContext.Current.CancellationToken);
        var body = await response.Content.ReadAsStringAsync(TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.OK, body);
        body.Should().Contain("\"database\"").And.Contain("\"cache\"").And.Contain("\"storage\"");
    }

    [Fact]
    public async Task Server_time_endpoint_returns_utc_now()
    {
        var before = DateTimeOffset.UtcNow;

        var response = await _client.GetFromJsonAsync<ServerTime>(
            new Uri("/api/v1/diagnostics/time", UriKind.Relative), TestContext.Current.CancellationToken);

        response.Should().NotBeNull();
        response!.UtcNow.Should().BeOnOrAfter(before).And.BeBefore(DateTimeOffset.UtcNow.AddSeconds(5));
    }

    [Fact]
    public async Task Unknown_routes_return_problem_details()
    {
        var response = await _client.GetAsync(new Uri("/api/v1/does-not-exist", UriKind.Relative), TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        response.Content.Headers.ContentType?.MediaType.Should().Be("application/problem+json");
    }

    private sealed record ServerTime(DateTimeOffset UtcNow);
}
