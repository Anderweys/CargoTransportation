using WebMVC.Models.DTOs;
using WebMVC.ViewModels.CargoViewModels;

namespace WebMVC.Services;

public interface ICargoService
{
    Task AddItemsAsync(UserItemsDTO userItems);
    Task<IEnumerable<CargoViewModel>?> GetCargoInfoAsync(string userId);
}
