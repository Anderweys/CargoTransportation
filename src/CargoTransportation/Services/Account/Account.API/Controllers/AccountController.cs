using MediatR;
using Microsoft.AspNetCore.Mvc; 
using Account.API.Application.Queries;
using Account.API.Application.Commands;

namespace Account.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AccountController : Controller
{
    private readonly IMediator _mediator;
    private readonly ILogger<AccountController> _logger;

    public AccountController(IMediator mediator, ILogger<AccountController> logger)
    {
        _mediator=mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger=logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpGet("getuser")]
    public async Task<IActionResult> GetUser([FromQuery] GetUserAccountQuery query)
    {
        var result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpPost("adduser")]
    public async Task<IActionResult> AddUser([FromBody] AddUserAccountCommand command)
    {
        var result = await _mediator.Send(command);

        if (result)
        {
            _logger.LogInformation(
                "Account was added successfully." +
                "\nCommand: {command}" +
                "\nDatetime: {datetime}",
                command,
                DateTime.UtcNow.ToLongTimeString());
        }

        return Ok(result);
    }
}
