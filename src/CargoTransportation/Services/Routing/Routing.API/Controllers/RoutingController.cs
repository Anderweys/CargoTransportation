using MediatR;
using Microsoft.AspNetCore.Mvc;
using Routing.API.Commands;

namespace Routing.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class RoutingController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<RoutingController> _logger;

    public RoutingController(IMediator mediator, ILogger<RoutingController> logger)
    {
        _mediator=mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger=logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpGet("GetDeliveryInfo")]
    public async Task<IActionResult> GetDeliveryInfo([FromQuery] GetDeliveryInfoCommand command)
    {
        var commandResult = await _mediator.Send(command);

        if (commandResult is null)
        {
            _logger.LogCritical(
                "Error in command: {command}, User id: {userId}",
                nameof(command),
                command.UserId);

            return BadRequest();
        }

        return Ok(commandResult);
    }

    [HttpPost("ConfirmCargoDelivery")]
    public async Task<IActionResult> ConfirmCargoDelivery([FromBody] CreateCargoCommand command)
    {
        var commandResult = await _mediator.Send(command);

        if (commandResult is false)
        {
            _logger.LogCritical(
                "Error in command: {command}, User id: {userId}",
                nameof(command),
                command.UserId);

            return BadRequest();
        }

        return Ok();
    }

    [HttpGet("LoadCities")]
    public async Task<IActionResult> LoadCities([FromQuery] LoadCitiesCommand command)
    {
        var commandResult = await _mediator.Send(command);

        if (commandResult is null)
        {
            _logger.LogCritical(
                "Error in service: {service}, command: {command}",
                nameof(command),
                nameof(RoutingController));

            return BadRequest();
        }

        return Ok(commandResult);
    }
}
