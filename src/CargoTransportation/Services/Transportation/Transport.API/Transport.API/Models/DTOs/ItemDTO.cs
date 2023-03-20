namespace Transportation.API.Models.DTOs;

public record ItemDTO(
    int Id, string Name, string Description,
    float Length, float Width, float Height, float Price);
