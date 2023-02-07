using Cargo.Domain.SeedWork;

namespace Cargo.Domain.AggregatesModel.LoaderAggragates;

public interface ILoaderRepository : IRepository<Loader>
{
    void Add(Loader loader);
    void Update(Loader loader);
    Task<Loader> GetById(int id);
}
