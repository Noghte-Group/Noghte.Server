using Noghte.BuildingBlock;

namespace Noghte.Application;

public class TestResponse : IMapping<Domain.Users.User>, IContract
{
    public string UserName { get; set; }
}