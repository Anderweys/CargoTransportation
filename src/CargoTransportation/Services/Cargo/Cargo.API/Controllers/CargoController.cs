using MediatR;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using CargoObject.Application.Models;
using CargoObject.Application.Queries.Query;
using CargoObject.Application.Commands.Command;

namespace CargoObject.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CargoController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<CargoController> _logger;

    public CargoController(IMediator mediator, ILogger<CargoController> logger)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpPost("AddItems")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> AddItems([FromBody] AddItemsInMemoryCacheCommand command)
    {
        var commandResult = await _mediator.Send(command);

        if (commandResult is false)
        {
            _logger.LogWarning(
                "Error in command: {command}, User id: {userId}",
                nameof(command),
                command.UserId);

            return BadRequest();
        }

        return Ok();
    }

    [HttpGet("GetCargoInfo")]
    [ProducesResponseType(typeof(IEnumerable<CargoInfo>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetCargoInfo([FromQuery] GetCargoInfoQuery query)
    {
        // It's maybe null, because user may haven't any cargo.
        var queryResult = await _mediator.Send(query);

        return Ok(queryResult);
    }
}