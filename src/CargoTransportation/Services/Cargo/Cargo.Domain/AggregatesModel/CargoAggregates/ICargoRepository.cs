using CargoObject.Domain.SeedWork;

namespace CargoObject.Domain.AggregatesModel.CargoAggregates;

public interface ICargoRepository : IRepository<Cargo>
{
    Task<Cargo> AddAsync(Cargo cargo);
    Task<IEnumerable<Cargo>> GetByUserIdAsync(string userId);
    Task<CargoType> GetCargoTypeAsync(float itemsVolume);
}
