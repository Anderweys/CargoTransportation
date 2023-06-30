using WebMVC.Models;
using WebMVC.Models.DTOs;
using WebMVC.ViewModels.RoutingViewModels;

namespace WebMVC.Services;

public interface IRoutingService
{
    Task<RoutingViewModel> GetDeliveryInfo(CityDTO cityDTO);
    Task<IEnumerable<CitiesName>> LoadCities();
    Task<bool> ConfirmCargoCreating(UserPaymentDTO userPaymentDTO);
}
