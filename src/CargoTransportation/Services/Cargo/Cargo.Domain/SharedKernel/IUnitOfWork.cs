namespace CargoObject.Domain.SeedWork;


public interface IUnitOfWork : IDisposable
{
    Task SaveChangesAsync(CancellationToken token = default(CancellationToken));
    Task SaveEntitiesAsync(CancellationToken token = default(CancellationToken));
}