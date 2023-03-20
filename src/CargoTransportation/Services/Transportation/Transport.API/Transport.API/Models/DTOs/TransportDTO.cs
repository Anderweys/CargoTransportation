﻿namespace Transportation.API.Models.DTOs;

public record TransportDTO(
    string Name,
    string Type,
    float AverageSpeed,
    float Volume);
