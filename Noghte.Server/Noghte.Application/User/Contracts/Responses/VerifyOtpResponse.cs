using Noghte.BuildingBlock;

namespace Noghte.Application.User.Contracts.Responses;

public class VerifyOtpResponse : IContract
{
    public string Token { get; set; }
}
