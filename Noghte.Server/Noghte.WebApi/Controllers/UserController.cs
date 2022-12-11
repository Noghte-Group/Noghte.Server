﻿using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Noghte.Application.User.Contracts;
using Noghte.Application.User.Contracts.Requests;
using Noghte.Application.User.Contracts.Responses;
using Noghte.BuildingBlock.ApiResponses;
using Noghte.Domain.Users;

namespace Noghte.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IUserRepository _userRepository;

    public UserController(IMediator mediator, IUserRepository userRepository)
    {
        _mediator = mediator;
        _userRepository = userRepository;
    }

    [HttpPost("sendOtp")]
    public async Task<IActionResult> SendOtp([FromQuery] SendOtpRequest model, CancellationToken cancellationToken) 
    {
        var request = _mediator.CreateRequestClient<SendOtpRequest>();
        
        var (accepted, rejected) = await request.GetResponse<ConsumerAccepted<SendOtpResponse>, ConsumerRejected>(model, cancellationToken);

        return new GenericResult<ConsumerAccepted<SendOtpResponse>>(accepted, rejected);
    }

    [HttpPost("verifyOtp")]
    public async Task<IActionResult> VerifyOtp(VerifyOtpRequest model, CancellationToken cancellationToken)
    {
        var request = _mediator.CreateRequestClient<VerifyOtpRequest>();

        var (accepted, rejected) = await request.GetResponse<ConsumerAccepted<VerifyOtpResponse>, ConsumerRejected>(model, cancellationToken);

        return new GenericResult<ConsumerAccepted<VerifyOtpResponse>>(accepted, rejected);
    }

    [HttpGet("get-all-users")]
    public async Task<IActionResult> GetUser(CancellationToken cancellationToken)
    {
        var users = await _userRepository.TableNoTracking.ToListAsync(cancellationToken);

        return Ok(users);
    }
}