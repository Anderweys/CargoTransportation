using WebMVC.Models;

namespace WebMVC.ViewModels.CargoViewModels;

public class IndexViewModel
{
    public User User { get; set; }
    public IEnumerable<Item> Items { get; set; } = new List<Item>();   
}
