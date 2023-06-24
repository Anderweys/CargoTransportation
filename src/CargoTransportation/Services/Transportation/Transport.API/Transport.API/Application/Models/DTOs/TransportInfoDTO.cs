namespace Transportation.API.Application.Models.DTOs;

public record TransportInfoDTO(
    string Transport,
    int CountCargo,
    float Price);
