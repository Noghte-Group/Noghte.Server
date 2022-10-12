using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Noghte.BuildingBlock.Exceptions;

namespace Noghte.BuildingBlock.ApiResponses;

public class GenericResult<T> : IActionResult
    where T : class
{
    private readonly Task<Response<T>>? _accepted;
    private readonly Task<Response<ConsumerRejected>> _rejected;

    public GenericResult(Task<Response<T>>? accepted, Task<Response<ConsumerRejected>> rejectedResponse)
    {
        _accepted = accepted;
        _rejected = rejectedResponse;
    }

    public async Task ExecuteResultAsync(ActionContext context)
    {
        if (_accepted is not null)
        {
            if (_accepted.IsCompletedSuccessfully)
            {
                var response = await _accepted;
                var objectResult = new ObjectResult(response.Message);
                await objectResult.ExecuteResultAsync(context);
            }
            else
            {
                var response = await _rejected;
                ObjectResult objectResult = response.Message.StatusCode switch
                {
                    ConsumerStatusCode.ServerError => new ObjectResult(response.Message) { StatusCode = 500 },
                    ConsumerStatusCode.UnAuthorized => new UnauthorizedObjectResult(response.Message),
                    ConsumerStatusCode.NotFound => new NotFoundObjectResult(response.Message),
                    ConsumerStatusCode.MethodNotAllowed => new ObjectResult(response.Message) { StatusCode = 405 },
                    _ => new BadRequestObjectResult(response.Message)
                };

                await objectResult.ExecuteResultAsync(context);
            }
        }
    }
}