using Noghte.BuildingBlock;

namespace Noghte.Application.User.Contracts;

public class SendLoginOtpRequest : IContract
{
    public string PhoneNumber { get; set; }
}
