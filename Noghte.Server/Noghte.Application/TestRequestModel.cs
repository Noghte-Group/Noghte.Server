using Noghte.BuildingBlock;

namespace Noghte.Application;

public class TestRequest : IMapping<Domain.Users.User> ,IContract
{
    public string UserName { get; set; }
}