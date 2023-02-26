using Microsoft.AspNetCore.Mvc;
using WebMVC.Models;
using WebMVC.Services;

namespace WebMVC.Controllers;

public class CargoController : Controller
{
    private readonly ILogger<CargoController> _logger;
    private ICargoService _cargoService;

    public CargoController(ILogger<CargoController> logger, ICargoService cargoService)
    {
        _logger=logger;
        _cargoService=cargoService;
    }

    public IActionResult Index() => View();

    [HttpPost]
    public async Task<IActionResult> Index([FromForm] User user)
    {
        var cargo = await _cargoService.GetCargo();
        _logger.LogInformation($"Query in CargoController: {user.Login}");

        return Ok(cargo);
    }
}
