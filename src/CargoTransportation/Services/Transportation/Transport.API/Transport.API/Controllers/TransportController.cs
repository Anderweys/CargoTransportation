using MediatR;
using Microsoft.AspNetCore.Mvc;
using Transportation.API.Commands.Command;
using Transportation.API.Models.DTOs;

namespace Transportation.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class TransportController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<TransportController> _logger;

    public TransportController(ILogger<TransportController> logger, IMediator mediator)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet("GetUserItems")]
    public async Task<IActionResult> GetUserItems([FromQuery] string userId)
    {
        var command = new GetUserItemsCommand(userId);
        var userItems = await _mediator.Send(command);

        if (userItems is null)
        {
            _logger.LogCritical(
                "Error in command: {command}, User id: {userId}",
                nameof(command),
                command.UserId);

            return BadRequest();
        }

        return Ok(userItems);
    }

    [HttpGet("GetTransportType")]
    public async Task<IActionResult> GetTransportType([FromQuery] GetTransportTypeCommand command)
    {
        var transportTypes = await _mediator.Send(command);

        if (transportTypes is null)
        {
            _logger.LogCritical(
                "Error in command: {command}",
                nameof(command));

            return BadRequest();
        }

        return Ok(transportTypes);
    }

    [HttpGet("GetTransportInfo")]
    public async Task<IActionResult> GetTransportInfo([FromQuery] TransportUserDTO dto)
    {
        var command = new GetTransportInfoCommand(dto.UserId, dto.Type, dto.Name);
        var transportInfo = await _mediator.Send(command);

        if (transportInfo is null)
        {
            _logger.LogCritical(
                "Error in command: {command}, User id: {userId}",
                nameof(command),
                command.UserId);

            return BadRequest();
        }

        return Ok(transportInfo);
    }
}