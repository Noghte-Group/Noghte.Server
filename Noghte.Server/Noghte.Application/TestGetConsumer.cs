using MassTransit;
using Noghte.BuildingBlock.ApiResponses;
using Noghte.BuildingBlock.Exceptions;

namespace Noghte.Application;

public class TestGetConsumer : IConsumer<TestRequest>
{
    public async Task Consume(ConsumeContext<TestRequest> context)
    {
        var request = context.Message;

        var mappedResult = new TestResponse
        {
            Title = request.Title
        };

        await context.RespondAsync(new ConsumerAccepted<TestResponse>
        {
            Result = mappedResult,
            Message = "Get SuccessFully",
            StatusCode = ConsumerStatusCode.Success
        });
    }
}