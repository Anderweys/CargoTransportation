namespace CargoObject.Application.Models;

public record class CargoInfo(
    string City,
    IEnumerable<Item> Items,
    DateTime Time,
    float Price
);
