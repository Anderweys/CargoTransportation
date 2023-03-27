/*
 *  Add default controller, because docker say: no endpoints... 
 */

using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ValuesController : ControllerBase
{
    [HttpGet("info")]
    public string Info()
    {
        return "Ocelot is alive!";
    }
}
