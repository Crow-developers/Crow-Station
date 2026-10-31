using CrowStation.Application.Abstractions.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace CrowStation.Application;

public static class DependencyInjection
{
    /// <summary>
    /// Registers every query and command handler in this assembly.
    /// </summary>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton(TimeProvider.System);

        Type[] handlerInterfaces =
        [
            typeof(IQueryHandler<,>),
            typeof(ICommandHandler<>),
            typeof(ICommandHandler<,>),
        ];

        var registrations =
            from type in typeof(DependencyInjection).Assembly.GetTypes()
            where type is { IsAbstract: false, IsInterface: false }
            from implemented in type.GetInterfaces()
            where implemented.IsGenericType && handlerInterfaces.Contains(implemented.GetGenericTypeDefinition())
            select (Service: implemented, Implementation: type);

        foreach (var (service, implementation) in registrations)
        {
            services.AddScoped(service, implementation);
        }

        return services;
    }
}
