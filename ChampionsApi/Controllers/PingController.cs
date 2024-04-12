using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChampionsApi.Controllers;

[Route("[controller]")]
[ApiController]
[AllowAnonymous]
public class PingController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<string>> Ping()
    {
        return Ok("pong");
    }
}
