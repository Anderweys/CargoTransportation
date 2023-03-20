namespace Routing.API.Models;

public interface ICityRepository
{
    Task<City> GetAsync(string name);
    Task<IEnumerable<City>> GetAllAsync();
}
