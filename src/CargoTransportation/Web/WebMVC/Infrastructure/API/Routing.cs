using WebMVC.Models.DTOs;

namespace WebMVC.Infrastructure.API;

public static partial class API
{
    public static class Routing
    {
        public static string GetDeliveryInfo(string baseUri, CityDTO cityDTO)
            => $"{baseUri}/getDeliveryInfo?city={cityDTO.City}&userId={cityDTO.UserId}";

        public static string LoadCities(string baseUri) => $"{baseUri}/LoadCities";

        public static string GetCargoAsync(string baseUri, string userId) => $"{baseUri}/getCargo/{userId}";

        public static string CreateCargo(string baseUri)
            => $"{baseUri}/confirmCargoDelivery";
    }
}