using AutoMapper;
using Noghte.BuildingBlock;

namespace Noghte.Application;

public class TestResponse : IMapping<Domain.Users.User>, IContract
{
    public string UserName { get; set; }

    public string TestMapper { get; set; }

    public override void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Users.User, TestResponse>()
            .ForMember(opt => opt.TestMapper, cfg => cfg.MapFrom(r => "testttttttt"));
    }
}