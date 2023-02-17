using Microsoft.AspNetCore.Mvc;
using WebMVC.Models;

namespace WebMVC.Controllers;

public class CargoController : Controller
{

    private readonly ILogger<CargoController> _logger;

    public CargoController(ILogger<CargoController> logger)
    {
        _logger=logger;
    }

    public IActionResult Index() => View();

    [HttpPost]
    public IActionResult Index([FromForm] User user)
    {
        _logger.LogInformation($"Query in CargoController: {user.Name} {user.Email}");

        // TODO: Get result from Cargo service.

        return Ok("cargo");
    }
}
