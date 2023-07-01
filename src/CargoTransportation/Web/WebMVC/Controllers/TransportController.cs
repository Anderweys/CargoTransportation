using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebMVC.Models;
using WebMVC.Services;
using WebMVC.Models.DTOs;
using WebMVC.Infrastructure;
using System.Net;
using WebMVC.ViewModels.CargoViewModels;

namespace WebMVC.Controllers;

[Authorize(AuthenticationSchemes = "AuthenticateJwt")]
public class TransportController : Controller
{
    private readonly ITransportService _transportService;

    public TransportController(ITransportService transportService)
    {
        _transportService = transportService ?? throw new ArgumentNullException(nameof(transportService));
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Item>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetUserItemsAsync([FromQuery] string token)
    {
        var userId = Authentication.ParseToken(token);
        var userItems = await _transportService.GetUserItems(userId);
        
        return Ok(userItems);
    }

    [HttpPost]
    [ProducesResponseType(typeof(TransportInfo), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> SelectTransport([FromBody] TransportType transportType, [FromQuery] string token)
    {
        var userId = Authentication.ParseToken(token);
        var transportTypeDTO = new TransportTypeDTO(userId, transportType.Name, transportType.Type);

        TransportInfo transportInfo = await _transportService.GetTransportInfo(transportTypeDTO);

        return Ok(transportInfo);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<TransportType>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> LoadTransport([FromQuery] string token)
    {
        var transportTypes = await _transportService.GetTransporType();

        return Ok(transportTypes);
    }
}
