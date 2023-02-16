using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cargo.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class CargoController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<CargoController> _logger;

    public CargoController()
    {

    }

}
