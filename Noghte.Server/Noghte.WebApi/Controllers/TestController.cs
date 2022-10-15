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
    private readonly IConnectionMultiplexer _redis;

    public TestController(IMediator mediator, IConnectionMultiplexer redis, IDistributedCache cache)
    {
        _mediator = mediator;
        _redis = redis;
        _cache = cache;
    }

    [HttpGet]
    public async Task<IActionResult> Test([FromQuery] TestRequest model, CancellationToken cancellationToken)
    {
        var content = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(model.Title));


        await _cache.SetAsync("Title", content,
            new DistributedCacheEntryOptions { AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(20) },
            cancellationToken);
        

        return Ok();
    }

    [HttpGet("key")]
    public async Task<IActionResult> GetValue(string key ,CancellationToken cancellationToken)
    {
        var bookContent = await _cache.GetStringAsync(key, cancellationToken);

        return Ok(bookContent);
    }
}