using CargoObject.Domain.SeedWork;

namespace CargoObject.Domain.ReadModels.CargoAggregates;

public interface ICargoReadRepository : IRepository<Cargo>
{
    Task<bool> AddAsync(Cargo cargo);
    Task<IEnumerable<Cargo>> GetByUserIdAsync(string Id);
    Task<CargoType> GetCargoTypeAsync(float itemsVolume);
}
