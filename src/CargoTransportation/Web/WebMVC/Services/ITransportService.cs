using WebMVC.Models;
using WebMVC.Models.DTOs;

namespace WebMVC.Services;

public interface ITransportService
{
    Task<IEnumerable<Item>> GetUserItems(string userId);
    Task<TransportInfo> GetTransportInfo(TransportTypeDTO transportTypeDTO);
    Task<IEnumerable<TransportType>> GetTransporType();
}
