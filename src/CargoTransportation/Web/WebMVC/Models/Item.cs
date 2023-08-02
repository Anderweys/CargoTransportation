namespace WebMVC.Models;

public record Item(
    Guid Id, string Name, string Description,
    float Length, float Width, float Height, float Price);