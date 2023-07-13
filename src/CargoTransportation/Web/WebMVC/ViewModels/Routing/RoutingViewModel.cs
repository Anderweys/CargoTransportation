namespace WebMVC.ViewModels.RoutingViewModels;

public record RoutingViewModel(
    string City,
    DateTime Time,
    IEnumerable<ItemModel> Items,
    float Price);