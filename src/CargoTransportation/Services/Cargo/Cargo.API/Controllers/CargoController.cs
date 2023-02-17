using CargoObject.API.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CargoObject.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class CargoController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<CargoController> _logger;

    public CargoController(ILogger<CargoController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public IActionResult GetCargo()
    {
        _logger.LogInformation("Gocha");
        var cargo = new Cargo() { Name = "lol", Email = "Kek" };
        return Ok(cargo);
    }

}
