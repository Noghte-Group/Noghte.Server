using Noghte.BuildingBlock;

namespace Noghte.Application.User.Contracts;

public class SendOtpRequest : IContract
{
    public string PhoneNumber { get; set; }
}
