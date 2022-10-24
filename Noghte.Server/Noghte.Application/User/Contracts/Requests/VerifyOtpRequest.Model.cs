using Noghte.BuildingBlock;

namespace Noghte.Application.User.Contracts.Requests;

public class VerifyOtpRequest : IContract
{
    public string Otp { get; set; }
    public string PhoneNumber { get; set; }
}