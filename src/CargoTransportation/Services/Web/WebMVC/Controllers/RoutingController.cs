using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebMVC.Models;
using WebMVC.Services;
using WebMVC.ViewModels;
using WebMVC.Infrastructure;

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
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SelectCity([FromBody] CityModel city, [FromQuery] string token)
    {
        var userId = Authentication.ParseToken(token);
        var viewModel = await _routingService.GetDeliveryInfo(new(userId, city.City));

        return Ok(viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> LoadCities([FromQuery] string token)
    {
        var cities = await _routingService.LoadCities();

        return Ok(cities);
    }

    [HttpPost]
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
