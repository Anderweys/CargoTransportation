namespace WebMVC.Infrastructure.API;

public static partial class API
{
    public static class Cargo
    {
        public static string AddItems(string baseUri) => $"{baseUri}/addItems";
        public static string GetCargo(string baseUri, string userId) => $"{baseUri}/getCargoInfo?userId={userId}";
    }
}