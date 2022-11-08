using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;
using Noghte.Application.User.Contracts;
using Noghte.Application.User.Contracts.Requests;
using Noghte.Application.User.Contracts.Responses;
using Noghte.BuildingBlock.ApiResponses;

namespace Noghte.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator) => _mediator = mediator;

    [HttpPost("send-Register-Otp")]
    public async Task<IActionResult> SendRegisterOtp([FromQuery] SendRegisterOtpRequest model, CancellationToken cancellationToken)
    {
        var request = _mediator.CreateRequestClient<SendRegisterOtpRequest>();

        var (accepted, rejected) = await request.GetResponse<ConsumerAccepted<SendOtpResponse>, ConsumerRejected>(model, cancellationToken);

        return new GenericResult<ConsumerAccepted<SendOtpResponse>>(accepted, rejected);
    }

    [HttpPost("send-Login-Otp")]
    public async Task<IActionResult> SendLoginOtp([FromQuery] SendLoginOtpRequest model, CancellationToken cancellationToken)
    {
        var request = _mediator.CreateRequestClient<SendLoginOtpRequest>();

        var (accepted, rejected) = await request.GetResponse<ConsumerAccepted<SendOtpResponse>, ConsumerRejected>(model, cancellationToken);

        return new GenericResult<ConsumerAccepted<SendOtpResponse>>(accepted, rejected);
    }

    [HttpPost("verify-Otp")]
    public async Task<IActionResult> VerifyOtp(VerifyOtpRequest model, CancellationToken cancellationToken)
    {
        var request = _mediator.CreateRequestClient<VerifyOtpRequest>();

        var (accepted, rejected) = await request.GetResponse<ConsumerAccepted<VerifyOtpResponse>, ConsumerRejected>(model, cancellationToken);

        return new GenericResult<ConsumerAccepted<VerifyOtpResponse>>(accepted, rejected);
    }
}