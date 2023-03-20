using WebMVC.Models.DTOs;

namespace WebMVC.Services;

public interface ICargoService
{
    Task AddItemsAsync(UserItemsDTO userItems);
    Task<IEnumerable<CargoInfoDTO>?> GetCargoInfoAsync(string userId);
}
