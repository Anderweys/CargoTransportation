﻿namespace WebMVC.ViewModels.RoutingViewModels;

public class RoutingViewModel
{
    public string City { get; set; }
    public DateTime Time { get; set; }
    public IEnumerable<ItemModel> Items { get; set; } = Enumerable.Empty<ItemModel>();
    public float Price { get; set; }
}
