using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebMVC.Controllers;

[Authorize]
public class AccountController : Controller
{
    private readonly ILogger<AccountController> _logger;

	public AccountController(ILogger<AccountController> logger)
	{
		_logger = logger ?? throw new ArgumentNullException(nameof(logger));
	}

	[Authorize]
	public async Task<IActionResult> SignIn(string str)
	{
		_logger.LogInformation("User enter.");
		return RedirectToAction("Index", "Cargo");
	}
}
