using System.Net;
using Microsoft.AspNetCore.Mvc;
using OneOf;

namespace Noghte.BuildingBlock.ApiResponses;

public class GenericResult<TAccepted> : IActionResult
    where TAccepted : class
{
    private readonly TAccepted _accepted;
    private readonly ConsumerRejected _rejectedResponse;

    public GenericResult(TAccepted accepted, ConsumerRejected rejectedResponse)
    {
        _accepted = accepted;
        _rejectedResponse = rejectedResponse;
    }

    public GenericResult(OneOf<TAccepted, ConsumerRejected> result)
    {
        result.TryPickT0(out _accepted, out _);
        result.TryPickT1(out _rejectedResponse, out _);
    }

    public async Task ExecuteResultAsync(ActionContext context)
    {
        if (_accepted is not null)
        {
            var objectResult = new ObjectResult(_accepted);
            await objectResult.ExecuteResultAsync(context);
        }
        else if (_rejectedResponse is not null)
        {
            ObjectResult objectResult = _rejectedResponse.HttpStatusCode switch
            {
                HttpStatusCode.OK => new OkObjectResult(_rejectedResponse),
                HttpStatusCode.Unauthorized => new UnauthorizedObjectResult(_rejectedResponse),
                HttpStatusCode.Conflict => new ConflictObjectResult(_rejectedResponse),
                HttpStatusCode.NotFound => new NotFoundObjectResult(_rejectedResponse),
                _ => new BadRequestObjectResult(_rejectedResponse)
            };

            await objectResult.ExecuteResultAsync(context);
        }
    }
}