using Microsoft.AspNetCore.Mvc;
using WebMVC.Models;

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

    [HttpGet]
    public IActionResult Index() => View();

    [HttpGet]
    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> SignIn([FromQuery] User user)
    {
        var result = await _userRepository.GetAsync(user);

        if (result is not null)
        {
            _logger.LogInformation(
                "User sign in.\n\tLogin: {Login}\n\tData: {DateTime}\n",
                user.Login, DateTime.UtcNow);

            return Ok(true);
        }

        return NoContent();
    }

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