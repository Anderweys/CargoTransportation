using Transportation.API.Models.DTOs;

namespace Transportation.API.Services;

public class CalculationPriceService
{
    private enum TransportTypeFactor : int
    {
        GROUND = 1,
        WATER = 2,
        AIR = 5
    }
    private const float CUSTOM_PERCENT = 0.25f;

    public float CalculatePrice(string type, IEnumerable<ItemDTO> itemsDTO)
    {
        var coefficient = type switch
        {
            "Ground" => TransportTypeFactor.GROUND,
            "Water" => TransportTypeFactor.WATER,
            "Air" => TransportTypeFactor.AIR,
            _ => throw new NotImplementedException("No transport type. Error in transport service!")
        };

        float coef = (float)coefficient;
        var totalPrice = itemsDTO.Sum(i => i.Price) * CUSTOM_PERCENT * coef;

        return totalPrice;
    }
}
