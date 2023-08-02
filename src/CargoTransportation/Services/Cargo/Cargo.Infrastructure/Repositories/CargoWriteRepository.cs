using Marten;
using CargoObject.Domain.AggregatesModel.CargoAggregates;

namespace CargoObject.Infrastructure.Repositories;


//TODO: Переделать под Marten
public class CargoWriteRepository : ICargoWriteRepository
{
    // Tут вместо контекста IDocumentSession.
    private readonly IDocumentSession _session;

    public CargoWriteRepository(IDocumentSession session)
    {
        _session = session;
    }

    public async Task<bool> AddAsync(Cargo cargo)
    {
        var result = _session.Events.Append(cargo.Id, cargo.GetUncommittedEvents());

        if (result is null)
        {
            return false;
        }

        await _session.SaveChangesAsync();
        cargo.ClearUncommittedEvents();

        return true;
    }
}
