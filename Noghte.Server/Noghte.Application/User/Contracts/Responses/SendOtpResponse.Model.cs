using Noghte.BuildingBlock;

namespace Noghte.Application.User.Contracts;

public class SendOtpResponse : IContract
{
    public string Otp { get; set; }
    public string PhoneNumber { get; set; }
}
