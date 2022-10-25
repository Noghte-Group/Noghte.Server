
using System.Text;
using System.Text.Json;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Noghte.Application;
using Noghte.BuildingBlock.ApiResponses;
using Noghte.Domain.Users;
using Noghte.Infrastructure;
using Noghte.Infrastructure.Users;
using StackExchange.Redis;

namespace Noghte.WebApi.Controllers;

[ApiController]
[Route("api/Test")]
public class TestController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IDistributedCache _cache;

    public TestController(IMediator mediator, IConnectionMultiplexer redis, IDistributedCache cache, IUserRepository userRepository)

    {
        _mediator = mediator;
        _cache = cache;
        _userRepository = userRepository;
    }


    [Authorization]
    [HttpGet]
    public async Task<IActionResult> Test([FromQuery] TestRequest model, CancellationToken cancellationToken)
    {
        var request = _mediator.CreateRequestClient<TestRequest>();

        var (accepted, rejected) = await request.GetResponse<ConsumerAccepted<TestResponse>, ConsumerRejected>(model, cancellationToken);


        return new GenericResult<ConsumerAccepted<TestResponse>>(accepted, rejected);
        //var content = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(model.UserName));

        //await _cache.SetAsync("Title", content,
            //new DistributedCacheEntryOptions { AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(20) },
            //cancellationToken);



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