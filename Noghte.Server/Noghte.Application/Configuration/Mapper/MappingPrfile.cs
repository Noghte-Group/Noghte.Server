using AutoMapper;
using Noghte.BuildingBlock;

namespace Noghte.Application.Configuration.Mapper;

public class MappingPrfile : Profile
{
    public MappingPrfile(IEnumerable<object> models)
    {
        foreach (var model in models)
        {
            var entity = model.GetType().BaseType.GetGenericArguments()[0];



            CreateMap(entity, model.GetType()).ReverseMap();
        }
    }
}
