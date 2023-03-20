using WebMVC.Models.DTOs;
using WebMVC.ViewModels.RoutingViewModels;

namespace WebMVC.Services;

public interface IRoutingService
{
    Task<IndexViewModel> GetDeliveryInfo(CityDTO cityDTO);
    Task<IEnumerable<CityNameDTO>> LoadCities();
    Task<bool> ConfirmCargoCreating(UserPaymentDTO userPaymentDTO);
}
