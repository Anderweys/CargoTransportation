using WebMVC.ViewModels;

namespace WebMVC.Models.DTOs;

public record class CargoInfoDTO(
    string City,
    IEnumerable<ItemModel> Items,
    DateTime Time,
    float Price
);