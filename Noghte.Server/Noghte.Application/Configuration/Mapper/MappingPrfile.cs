using AutoMapper;

namespace Noghte.Application.Configuration.Mapper;

public class MappingPrfile : Profile
{
    public MappingPrfile(IEnumerable<Type> contracts)
    {
        foreach (var contract in contracts)
        {
            var entityModel = contract.BaseType != null && contract.BaseType.Name.Contains("IMapping") ?
                contract.BaseType.GenericTypeArguments[0] : null;

            if (entityModel == null) continue;

            var instance = Activator.CreateInstance(contract);

            var methodInfo = contract.GetMethod("Mapping") ?? null;

            if (methodInfo != null && methodInfo.GetBaseDefinition().DeclaringType != methodInfo.DeclaringType)
                methodInfo?.Invoke(instance, new object[] { this });
            else
                CreateMap(entityModel, contract).ReverseMap();
        }
    }
}
