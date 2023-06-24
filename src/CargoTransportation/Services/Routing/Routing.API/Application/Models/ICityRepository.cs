namespace Routing.API.Application.Models;

public interface ICityRepository
{
    Task<City> GetAsync(string name);
    Task<IEnumerable<City>> GetAllAsync();
}
