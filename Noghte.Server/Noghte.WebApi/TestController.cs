using Microsoft.AspNetCore.Mvc;

namespace Noghte.WebApi;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok();
    }
    
    [HttpGet("hello")]
    public IActionResult Get2()
    {
        return Ok();
    }
}