using Microsoft.Extensions.DependencyInjection;
using Noghte.BuildingBlock.LifeCycle;

namespace Noghte.BuildingBlock.Extensions;

/// <summary>
/// Inject classes that implement
/// ISingletonDependency,IScopedDependency,ITransientDependency
/// To Service Collection 
/// </summary>
public static class ServiceCollectionExtensions
{
    public static void InjectLifeCycles(this IServiceCollection serviceCollection)
    {
        var singletonTypes = ReflectionExtensions.LoadTypesFromAssemblies(p =>
            p.IsClass && !p.IsGenericType && typeof(ISingletonDependency).IsAssignableFrom(p));

        var transientTypes = ReflectionExtensions.LoadTypesFromAssemblies(p =>
            p.IsClass && !p.IsGenericType && typeof(ITransientDependency).IsAssignableFrom(p));

        var scopedTypes = ReflectionExtensions.LoadTypesFromAssemblies(p =>
            p.IsClass && !p.IsGenericType && typeof(IScopedDependency).IsAssignableFrom(p));

        foreach (var type in singletonTypes) Inject(type, "AddSingleton");
        foreach (var type in scopedTypes) Inject(type, "AddScoped");
        foreach (var type in transientTypes) Inject(type, "AddTransient");

        void Inject(Type type, string injectType)
        {
            var @interface = type.GetInterface($"I{type.Name}");
            if (@interface is not null)
            {
                switch (@injectType)
                {
                    case "AddScoped":
                        serviceCollection.AddScoped(@interface, type);
                        break;
                    case "AddTransient":
                        serviceCollection.AddTransient(@interface, type);
                        break;

                    case "AddSingleton":
                        serviceCollection.AddSingleton(@interface, type);
                        break;
                }
            }
        }

    }
}
