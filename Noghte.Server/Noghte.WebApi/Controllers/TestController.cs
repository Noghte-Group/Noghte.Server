using System.Text;
using System.Text.Json;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Noghte.Application;
using Noghte.BuildingBlock.ApiResponses;
using StackExchange.Redis;

namespace Noghte.WebApi.Controllers;

[ApiController]
[Route("api/Test")]
public class TestController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IDistributedCache _cache;

    public TestController(IMediator mediator, IDistributedCache cache)
    {
        _mediator = mediator;
        _cache = cache;
    }

    [HttpGet]
    public async Task<IActionResult> Test([FromQuery] TestRequest model, CancellationToken cancellationToken)
    {
        var request = _mediator.CreateRequestClient<TestRequest>();

        var (accepted, rejected) = await request.GetResponse<ConsumerAccepted<TestResponse>, ConsumerRejected>(model, cancellationToken);

        return new GenericResult<ConsumerAccepted<TestResponse>>(accepted, rejected);
        //var content = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(model.UserName));


        //await _cache.SetAsync("Title", content,
        //    new DistributedCacheEntryOptions { AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(20) },
        //    cancellationToken);


        //return Ok();
    }

    [HttpGet("key")]
    public async Task<IActionResult> GetValue(string key, CancellationToken cancellationToken)
    {
        var bookContent = await _cache.GetStringAsync(key, cancellationToken);

        return Ok(bookContent);
    }
}