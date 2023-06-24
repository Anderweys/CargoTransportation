namespace Transportation.API.Application.Models.DTOs;

public record TransportDTO(
    string Name,
    string Type,
    float AverageSpeed,
    float Volume);
