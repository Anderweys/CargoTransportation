namespace Routing.API.Application.Models.DTOs;

public record DeliveryInfoDTO(
    string City,
    DateTime Time,
    IEnumerable<ItemInfo> ItemInfos,
    float Price);