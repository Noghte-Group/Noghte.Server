using MassTransit;
using Microsoft.Extensions.Caching.Distributed;
using Noghte.Application.User.Contracts.Requests;
using Noghte.Application.User.Contracts.Responses;
using Noghte.BuildingBlock;
using Noghte.BuildingBlock.ApiResponses;
using Noghte.BuildingBlock.ConsumerMessages;
using Noghte.Domain.Users;
using System.Text;
using System.Text.RegularExpressions;

namespace Noghte.Application.User.Consumers;

public class VerifyOtpConsumer : IConsumer<VerifyOtpRequest>
{
    private readonly IUserRepository _userRepository;
    private readonly IDistributedCache _cache;

    public VerifyOtpConsumer(IUserRepository userRepository, IDistributedCache cache)
    {
        _userRepository = userRepository;
        _cache = cache;
    }


    public async Task Consume(ConsumeContext<VerifyOtpRequest> context)
    {
        var request = context.Message;
        var cancellationToken = context.CancellationToken;

        var fetchedOtp = await _cache.GetAsync(request.PhoneNumber, cancellationToken);


        if (fetchedOtp is null)
        {
            await context.RespondAsync(new ConsumerRejected
            {
                Message = "user_otp_is_expired",
                StatusCode = ConsumerStatusCode.BadRequest
            });
            return;
        }

        var otp = Regex.Match(Encoding.UTF8.GetString(fetchedOtp), @"\d+").Value;


        if (!request.Otp.Equals(otp))
        {
            await context.RespondAsync(new ConsumerRejected
            {
                Message = "user_otp_is_not_valid",
                StatusCode = ConsumerStatusCode.BadRequest
            });
            return;
        }

        var userForCreate = new Domain.Users.User
        {
            PhoneNumber = request.PhoneNumber,
            LastLoginDate = DateTime.Now,
        };

        var token = await _userRepository.GenerateUserWithTokenAsync(userForCreate, cancellationToken);

        var mappedResult = new VerifyOtpResponse
        {
            Token = token
        };


        await context.RespondAsync(new ConsumerAccepted<VerifyOtpResponse>
        {
            Result = mappedResult,
            Message = ConsumerMessage.CREATE_SUCCESSFULLY("User"),
            StatusCode = ConsumerStatusCode.Success
        });
    }
}
