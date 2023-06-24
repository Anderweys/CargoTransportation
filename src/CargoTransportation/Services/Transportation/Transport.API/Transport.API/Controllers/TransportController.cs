using MediatR;
using Microsoft.AspNetCore.Mvc;
using Transportation.API.Application.Models.DTOs;
using Transportation.API.Application.Queries.Query;

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
        var query = new GetUserItemsQuery(userId);
        var userItems = await _mediator.Send(query);

        if (userItems is null)
        {
            _logger.LogCritical(
                "Error in query: {query}, User id: {userId}",
                nameof(query),
                query.UserId);

            return BadRequest();
        }

        return Ok(userItems);
    }

    [HttpGet("GetTransportType")]
    public async Task<IActionResult> GetTransportType([FromQuery] GetTransportTypeQuery query)
    {
        var transportTypes = await _mediator.Send(query);

        if (transportTypes is null)
        {
            _logger.LogCritical(
                "Error in query: {query}",
                nameof(query));

            return BadRequest();
        }

        return Ok(transportTypes);
    }

    [HttpGet("GetTransportInfo")]
    public async Task<IActionResult> GetTransportInfo([FromQuery] TransportUserDTO dto)
    {
        var query = new GetTransportInfoQuery(dto.UserId, dto.Type, dto.Name);
        var transportInfo = await _mediator.Send(query);

        if (transportInfo is null)
        {
            _logger.LogCritical(
                "Error in query: {query}, User id: {userId}",
                nameof(query),
                query.UserId);

            return BadRequest();
        }

        return Ok(transportInfo);
    }
}