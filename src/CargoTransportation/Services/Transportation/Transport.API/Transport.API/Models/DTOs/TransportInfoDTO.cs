namespace Transportation.API.Models.DTOs;

public record TransportInfoDTO(
    string Transport, 
    int CountCargo, 
    float Price);
