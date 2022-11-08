using AutoMapper;
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

public class SendLoginOtpConsumer : IConsumer<SendLoginOtpRequest>
{
    private readonly IUserRepository _userRepository;
    private readonly IDistributedCache _cache;
    private readonly IMapper _mapper;

    public SendLoginOtpConsumer(IUserRepository userRepository, IDistributedCache cache, IMapper mapper)
    {
        _userRepository = userRepository;
        _cache = cache;
        _mapper = mapper;
    }

    public async Task Consume(ConsumeContext<SendLoginOtpRequest> context)
    {
        var request = context.Message;
        var cancellationToken = context.CancellationToken;

        var user = await _userRepository.TableNoTracking.FirstOrDefaultAsync(r => r.PhoneNumber.Equals(request.PhoneNumber), cancellationToken);

        if (user is null)
        {
            await context.RespondAsync(new ConsumerRejected
            {
                Message = ConsumerMessage.NOTFOUND("User"),
                StatusCode = ConsumerStatusCode.NotFound
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
            Message = ConsumerMessage.Sent_Successfully("Otp"),
            StatusCode = ConsumerStatusCode.Success
        });
    }
}
