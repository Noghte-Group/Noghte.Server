using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Noghte.Application.User.Contracts;
using Noghte.BuildingBlock;
using Noghte.BuildingBlock.ApiResponses;
using Noghte.BuildingBlock.ConsumerMessages;
using Noghte.BuildingBlock.Helpers;
using Noghte.Domain.Users;
using System.Text;
using System.Text.Json;

namespace Noghte.Application.User.Consumers;

public class SendRegisterOtpConsumer : IConsumer<SendOtpRequest>
{

    private readonly IUserRepository _userRepository;
    private readonly IDistributedCache _cache;

    public SendRegisterOtpConsumer(IUserRepository userRepository, IDistributedCache cache)
    {
        _userRepository = userRepository;
        _cache = cache;
    }

    public async Task Consume(ConsumeContext<SendOtpRequest> context)
    {
        var request = context.Message;
        var cancellationToken = context.CancellationToken;

        var user = await _userRepository.TableNoTracking
            .FirstOrDefaultAsync(x => x.PhoneNumber.Equals(request.PhoneNumber), cancellationToken);

        if (user is not null)
        {
            await context.RespondAsync(new ConsumerRejected
            {
                Message = ConsumerMessage.DUPLICATED("user"),
                StatusCode = ConsumerStatusCode.BadRequest
            });
            return;
        }

        var otp = Generator.GenerateOTP();


        var content = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(otp));


        await _cache.SetAsync(request.PhoneNumber, content,
            new DistributedCacheEntryOptions { AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(2) },
            cancellationToken);

        var mappedResult = new SendOtpResponse
        {
            Otp = otp,
            PhoneNumber = request.PhoneNumber,
        };


        await context.RespondAsync(new ConsumerAccepted<SendOtpResponse>
        {
            Result = mappedResult,
            Message = ConsumerMessage.Sent_Successfully(),
            StatusCode = ConsumerStatusCode.Success
        });
    }
}
