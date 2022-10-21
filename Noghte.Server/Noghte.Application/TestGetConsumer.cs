using AutoMapper;
using MassTransit;
using Noghte.BuildingBlock;
using Noghte.BuildingBlock.ApiResponses;

namespace Noghte.Application;

public class TestGetConsumer : IConsumer<TestRequest>
{
    private readonly IMapper _mapper;

    public TestGetConsumer(IMapper mapper)
    {
        _mapper = mapper;
    }

    public async Task Consume(ConsumeContext<TestRequest> context)
    {
        var request = context.Message;

        var modelForCreate = _mapper.Map<Domain.Users.User>(request);

        var mappedResult = _mapper.Map<TestResponse>(modelForCreate);

        await context.RespondAsync(new ConsumerAccepted<TestResponse>
        {
            Result = mappedResult,
            Message = "Get SuccessFully",
            StatusCode = ConsumerStatusCode.Success
        });
    }
}