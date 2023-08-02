namespace Transportation.API.Application.Models.DTOs;

public record ItemDTO(
    Guid Id, string Name, string Description,
    float Length, float Width, float Height, float Price);
