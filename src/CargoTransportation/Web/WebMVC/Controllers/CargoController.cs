using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebMVC.Models;
using System.Net;
using WebMVC.Services;
using WebMVC.Models.DTOs;
using WebMVC.Infrastructure;
using WebMVC.ViewModels.CargoViewModels;

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
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public IActionResult Index() => View();

    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public IActionResult MyCargo() => View();

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Accepted)]
    public async Task<IActionResult> PrepareAddedItems([FromBody] List<Item> items, [FromQuery] string token)
    {
        var userId = Authentication.ParseToken(token);
        var userItems = new UserItemsDTO(userId, items);

        await _cargoService.AddItemsAsync(userItems);

        return Accepted();
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CargoViewModel>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetCargoInfo([FromQuery] string token)
    {
        var userId = Authentication.ParseToken(token);
        var cargoInfoViewModel = await _cargoService.GetCargoInfoAsync(userId);

        return Ok(cargoInfoViewModel);
    }
}

