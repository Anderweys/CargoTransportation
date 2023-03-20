using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebMVC.Models;
using WebMVC.Infrastructure;

namespace WebMVC.Controllers;

public class AccountController : Controller
{
    private readonly ILogger<AccountController> _logger;
    private readonly IUserRepository _userRepository;

    public AccountController(ILogger<AccountController> logger, IUserRepository userRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult Index() => View();

    [AllowAnonymous]
    [HttpGet]
    public IActionResult Create() => View();

    [Authorize(AuthenticationSchemes = "AuthenticateJwt")]
    [HttpGet]
    public IActionResult MainPage() => View();

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> SignIn([FromQuery] User user)
    {
        var result = await _userRepository.GetAsync(user);

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
    public async Task<IActionResult> Create([FromQuery] User user)
    {
        var isCreated = await _userRepository.AddAsync(user);

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