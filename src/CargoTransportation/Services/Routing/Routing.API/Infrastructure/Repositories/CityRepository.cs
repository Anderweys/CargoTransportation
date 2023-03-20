using MongoDB.Driver;
using Routing.API.Models;

namespace Routing.API.Infrastructure.Repositories;

public class CityRepository : ICityRepository
{
    private readonly IMongoCollection<City> _cities;
    private readonly ILogger<CityRepository> _logger;

    public CityRepository(IMongoDatabase mongo, ILogger<CityRepository> logger)
    {
        _cities= mongo.GetCollection<City>("Cities");
        _logger=logger;
        
        InitData();
    }

    public async Task<IEnumerable<City>> GetAllAsync()
        => await _cities.FindAsync(_ => true).Result.ToListAsync();

    public async Task<City> GetAsync(string name)
        => await _cities.FindAsync(c => c.Name == name).Result.FirstOrDefaultAsync();

    private void InitData()
    {
        if (_cities.CountDocuments("{}") is not 0)
        {
            return;
        }

        // Fake distance for each city.
        Random rand = new(DateTime.UtcNow.Second);

        _cities.InsertMany(new List<City>()
        {
            new("Moscow", rand.Next(1000, 7000)),
            new("SaintPetersburg", rand.Next(1000, 7000)),
            new("Omsk", rand.Next(1000, 7000)),
            new("Krasnodar", rand.Next(1000, 7000)),
            new("Rostov-on-Don", rand.Next(1000, 7000)),
            new("Ufa", rand.Next(1000, 7000)),
            new("Yekaterinburg", rand.Next(1000, 7000)),
            new("Chelyabinsk", rand.Next(1000, 7000)),
            new("Yaroslavl", rand.Next(1000, 7000))
        });
    }
}
