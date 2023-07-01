using WebMVC.Models.DTOs;

namespace WebMVC.Infrastructure.API;

public static partial class API
{
    public static class Transport
    {
        public static string GetUserItems(string baseUri, string userId) => $"{baseUri}/getUserItems?userId={userId}";
        public static string GetTransportInfo(string baseUri, TransportTypeDTO dto) 
            => $"{baseUri}/getTransportInfo?userId={dto.UserId}&name={dto.Name}&type={dto.Type}";
        public static string GetTransportType(string baseUri) => $"{baseUri}/getTransportType";
    }
}