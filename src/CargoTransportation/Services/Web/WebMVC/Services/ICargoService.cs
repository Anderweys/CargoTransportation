using WebMVC.ViewModels;

namespace WebMVC.Services
{
    public interface ICargoService
    {
        Task<Cargo> GetCargo();
    }
}
