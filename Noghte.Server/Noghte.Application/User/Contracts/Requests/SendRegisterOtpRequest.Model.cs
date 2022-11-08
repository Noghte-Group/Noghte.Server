using Noghte.BuildingBlock;

namespace Noghte.Application.User.Contracts;

public class SendRegisterOtpRequest : IContract
{
    public string PhoneNumber { get; set; }
}
