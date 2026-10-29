using System.Reflection;
using NetArchTest.Rules;

namespace CrowStation.Architecture.Tests;

/// <summary>
/// Enforces the Clean Architecture dependency rule: dependencies point inward only.
/// Api -> Infrastructure -> Application -> Domain.
/// </summary>
public sealed class LayerDependencyTests
{
    private const string DomainNamespace = "CrowStation.Domain";
    private const string ApplicationNamespace = "CrowStation.Application";
    private const string InfrastructureNamespace = "CrowStation.Infrastructure";
    private const string ApiNamespace = "CrowStation.Api";

    private static readonly Assembly Domain = typeof(Domain.Common.Result).Assembly;
    private static readonly Assembly Application = typeof(Application.DependencyInjection).Assembly;
    private static readonly Assembly Infrastructure = typeof(Infrastructure.DependencyInjection).Assembly;

    [Fact]
    public void Domain_depends_on_no_other_layer_or_framework() =>
        AssertNoDependency(Domain, ApplicationNamespace, InfrastructureNamespace, ApiNamespace,
            "Microsoft.EntityFrameworkCore", "Microsoft.AspNetCore", "StackExchange.Redis", "Amazon");

    [Fact]
    public void Application_does_not_depend_on_infrastructure_or_api() =>
        AssertNoDependency(Application, InfrastructureNamespace, ApiNamespace,
            "Microsoft.EntityFrameworkCore", "Microsoft.AspNetCore", "StackExchange.Redis", "Amazon");

    [Fact]
    public void Infrastructure_does_not_depend_on_api() =>
        AssertNoDependency(Infrastructure, ApiNamespace);

    [Fact]
    public void Handlers_are_internal_and_sealed()
    {
        var result = Types.InAssembly(Application)
            .That().HaveNameEndingWith("Handler", StringComparison.Ordinal)
            .And().AreClasses()
            .Should().BeSealed().And().NotBePublic()
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }

    [Fact]
    public void Domain_types_live_in_the_domain_namespace()
    {
        var result = Types.InAssembly(Domain).Should().ResideInNamespaceStartingWith(DomainNamespace).GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }

    private static void AssertNoDependency(Assembly assembly, params string[] forbidden)
    {
        var result = Types.InAssembly(assembly).ShouldNot().HaveDependencyOnAny(forbidden).GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }
}
