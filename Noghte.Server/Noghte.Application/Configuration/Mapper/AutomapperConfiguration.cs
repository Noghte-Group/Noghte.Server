using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Noghte.BuildingBlock;
using System.Reflection;

namespace Noghte.Application.Configuration.Mapper;

public static class AutomapperConfiguration
{
    public static void InitializeAutoMapper(this IServiceCollection services, params Assembly[] assemblies)
    {

        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddCustomMappingProfile();
        });

        var mapper = config.CreateMapper();

        services.AddSingleton(mapper);
    }

    public static void AddCustomMappingProfile(this IMapperConfigurationExpression config)
    {
        config.AddCustomMappingProfile(Assembly.GetExecutingAssembly());
    }

    public static void AddCustomMappingProfile(this IMapperConfigurationExpression config, Assembly assembly)
    {
        var allTypes = assembly.ExportedTypes;

        var list = allTypes.Where(type => type.IsClass && !type.IsAbstract &&
        type.BaseType.IsGenericType && type.GetInterfaces().Contains(typeof(IContract)))
            .Select(r => Activator.CreateInstance(r));

        var profile = new MappingPrfile(list);

        config.AddProfile(profile);
    }
}
