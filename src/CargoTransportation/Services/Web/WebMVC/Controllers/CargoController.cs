using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebMVC.Models;
using WebMVC.Services;
using WebMVC.Models.DTOs;
using WebMVC.Infrastructure;

namespace WebMVC.Controllers;

[Authorize(AuthenticationSchemes = "AuthenticateJwt")]
public class CargoController : Controller
{
    private readonly ICargoService _cargoService;

    public CargoController(ICargoService cargoService)
    {
        _cargoService=cargoService ?? throw new ArgumentNullException(nameof(cargoService));
    }

    [HttpGet]
    public IActionResult Index() => View();

    [HttpGet]
    public IActionResult MyCargo() => View();

    [HttpPost]
    public async Task<IActionResult> PrepareAddedItems([FromBody] List<Item> items, [FromQuery] string token)
    {
        var userId = Authentication.ParseToken(token);
        var userItems = new UserItemsDTO(userId, items);

        await _cargoService.AddItemsAsync(userItems);

        return Accepted();
    }

    [HttpGet]
    public async Task<IActionResult> GetCargoInfo([FromQuery] string token)
    {
        var userId = Authentication.ParseToken(token);
        var cargoInfo = await _cargoService.GetCargoInfoAsync(userId);

        return Ok(cargoInfo);
    }
}

