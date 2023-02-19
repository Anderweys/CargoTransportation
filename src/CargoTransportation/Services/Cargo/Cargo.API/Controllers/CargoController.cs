using CargoObject.API.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CargoObject.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class CargoController : ControllerBase
{
    private readonly ILogger<CargoController> _logger;

    public CargoController(ILogger<CargoController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult GetCargo()
    {
        var cargo = new Cargo() { Name = "lol", Email = "Kek" };
        _logger.LogInformation($"Gocha: Name: {cargo.Name} Email: {cargo.Email}");
        return Ok(cargo);
    }

}
