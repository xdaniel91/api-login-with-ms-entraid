using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChampionsApi.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class ChampionController : ControllerBase
{
    
    [HttpGet()]
    public async Task<ActionResult<IEnumerable<string>>> Get()
    {
        return Ok(Database.Champions);
    }
}