using Microsoft.AspNetCore.Authorization;
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
    public async Task<IActionResult> SignIn([FromForm] User user)
    {
        var result = await _userRepository.GetAsync(user);

        if (result is not null)
        {
            _logger.LogInformation(
                "User sign in.\n\tName: {Name}\n\tEmail: {Email}\n\tData: {DateTime}\n",
                user.Name, user.Email, DateTime.UtcNow);

            return RedirectToAction(nameof(CargoController.Index), "cargo");
        }

        //TODO: Add AJAX.
        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] User user)
    {
        var isCreated = await _userRepository.AddAsync(user);

        if (isCreated)
        {
            _logger.LogInformation(
                "User created.\n\tName: {Name}\n\tEmail: {Email}\n\tData: {DateTime}\n",
                user.Name, user.Email, DateTime.UtcNow);

            return RedirectToAction(nameof(AccountController.Index));
        }

        //TODO: Add AJAX.
        return NotFound();
    }
}
