using Microsoft.AspNetCore.Mvc;
using WebMVC.Models;

namespace WebMVC.Controllers;

public class CargoController : Controller
{
    public IActionResult Index() => View();

    // TODO.
    //public string Index(User user, IEnumerable<Item> items)
    //{
    //    return Ok();
    //}
}
