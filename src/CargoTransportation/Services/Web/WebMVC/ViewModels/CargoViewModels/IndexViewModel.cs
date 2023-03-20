using WebMVC.Models;

namespace WebMVC.ViewModels.CargoViewModels;

public class IndexViewModel
{
    public string City { get; set; }
    public IEnumerable<ItemModel> Items { get; set; } = new List<ItemModel>();
    public DateTime Time { get; set; }
    public float Price { get; set; }
}
