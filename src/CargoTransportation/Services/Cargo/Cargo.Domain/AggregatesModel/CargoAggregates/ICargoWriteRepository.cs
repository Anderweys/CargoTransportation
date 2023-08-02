using CargoObject.Domain.SeedWork;

namespace CargoObject.Domain.AggregatesModel.CargoAggregates;

public interface ICargoWriteRepository : IRepository<Cargo>
{
    Task<bool> AddAsync(Cargo cargo);
}
