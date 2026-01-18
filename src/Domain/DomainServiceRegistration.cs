using System.Reflection;
using Core.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Domain;

public static class DomainServiceRegistration
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        // Business Rules Domain
        services.AddSubClassesOfType(Assembly.GetExecutingAssembly(), typeof(IDomainService));

        return services;
    }
    
    private static IServiceCollection AddSubClassesOfType(
        this IServiceCollection services,
        Assembly assembly,
        Type type,
        Func<IServiceCollection, Type, IServiceCollection>? addWithLifeCycle = null
    )
    {
        var types = assembly.GetTypes()
            .Where(t => type.IsAssignableFrom(t) && t.IsClass && !t.IsAbstract)
            .ToList();
        
        foreach (Type? item in types)
            if (addWithLifeCycle == null)
                services.AddScoped(item);
            else
                addWithLifeCycle(services, type);
        return services;
    }
}