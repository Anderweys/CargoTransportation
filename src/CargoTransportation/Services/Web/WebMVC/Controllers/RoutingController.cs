using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebMVC.Models;
using WebMVC.Services;
using WebMVC.ViewModels;
using WebMVC.Infrastructure;
using WebMVC.ViewModels.CargoViewModels;
using WebMVC.ViewModels.RoutingViewModels;

namespace WebMVC.Controllers;

[Authorize(AuthenticationSchemes = "AuthenticateJwt")]
public class RoutingController : Controller
{
    private readonly IRoutingService _routingService;

    public RoutingController(IRoutingService routingService)
    {
        _routingService=routingService ?? throw new ArgumentNullException(nameof(routingService));
    }

    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public IActionResult Index() => View();

    [HttpPost]
    [ProducesResponseType(typeof(RoutingViewModel), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> SelectCity([FromBody] CityModel city, [FromQuery] string token)
    {
        var userId = Authentication.ParseToken(token);
        var routingViewModel = await _routingService.GetDeliveryInfo(new(userId, city.City));

        return Ok(routingViewModel);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CitiesName>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> LoadCities([FromQuery] string token)
    {
        var cities = await _routingService.LoadCities();

        return Ok(cities);
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Confirm([FromBody] Payment payment, [FromQuery] string token)
    {
        var userId = Authentication.ParseToken(token);
        var isCreated = await _routingService.ConfirmCargoCreating(new(userId, payment.City, payment.Time, payment.Money));

        if(!isCreated)
        {
            return BadRequest();
        }

        return Ok();
    }
}
