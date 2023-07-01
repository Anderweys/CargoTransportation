namespace WebMVC.ViewModels.CargoViewModels;

public record class CargoViewModel(
    string City,
    IEnumerable<ItemModel> Items,
    DateTime Time,
    float Price
);
