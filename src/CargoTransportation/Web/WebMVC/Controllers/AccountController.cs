using System.Net;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebMVC.Models;
using WebMVC.Infrastructure;
using WebMVC.Services;

namespace WebMVC.Controllers;

public class AccountController : Controller
{
    private readonly ILogger<AccountController> _logger;
    private readonly IAccountService _accountService;

    public AccountController(ILogger<AccountController> logger, IAccountService accountService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
    }

    [AllowAnonymous]
    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public IActionResult Index() => View();

    [AllowAnonymous]
    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public IActionResult Create() => View();

    [Authorize(AuthenticationSchemes = "AuthenticateJwt")]
    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public IActionResult MainPage() => View();

    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> SignIn([FromQuery] User user)
    {
        var result = await _accountService.GetAsync(user);

        if (result is not null)
        {
            _logger.LogInformation(
                "User sign in.\n\tLogin: {Login}\n\tData: {DateTime}\n",
                user.Login, DateTime.UtcNow);

            var jwt = Authentication.CreateToken(user.Login);

            return Ok(new { IsCreated = true, Token = new JwtSecurityTokenHandler().WriteToken(jwt) });
        }

        return Ok(new { IsCreated = false });
    }

    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(typeof(bool),(int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> Create([FromQuery] User user)
    {
        var isCreated = await _accountService.AddAsync(user);

        if (isCreated)
        {
            _logger.LogInformation(
                "User created.\n\tLogin: {Login}\n\tData: {DateTime}\n",
                user.Login, DateTime.UtcNow);

            return Ok(true);
        }

        return NoContent();
    }
}