using Cargo.Domain.SeedWork;

namespace Cargo.Domain.AggregatesModel.CargoAggregates;


public interface ICargoRepository : IRepository<Cargo>
{
    void Add(Cargo cargo);
    void Update(Cargo cargo);
    void RemoveAt(int id);
    Task<Cargo> GetAsyncById(int id);
    Task<IEnumerable<Cargo>> GetAllAsync();
}
