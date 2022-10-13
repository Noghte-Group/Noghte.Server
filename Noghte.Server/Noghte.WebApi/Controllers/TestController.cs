using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;
using Noghte.Application;
using Noghte.BuildingBlock.ApiResponses;

namespace Noghte.WebApi.Controllers;

[ApiController]
[Route("api/Test")]
public class TestController : ControllerBase
{
    private readonly IMediator _mediator;

    public TestController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> Test([FromQuery] TestRequest model, CancellationToken cancellationToken)
    {
        var request = _mediator.CreateRequestClient<TestRequest>();

        var (accepted, rejected) =
            await request.GetResponse<ConsumerAccepted<TestResponse>, ConsumerRejected>(model, cancellationToken);

        return new GenericResult<ConsumerAccepted<TestResponse>>(accepted, rejected);
    }
}