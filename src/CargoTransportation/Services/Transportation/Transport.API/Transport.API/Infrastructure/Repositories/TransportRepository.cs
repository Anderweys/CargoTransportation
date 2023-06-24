using MediatR;
using MongoDB.Driver;
using Transportation.API.Application.Models;

namespace Transportation.API.Infrastructure.Repositories;

public class TransportRepository : ITransportRepository
{
    private readonly IMongoCollection<Transport> _transports;

    public TransportRepository(IMongoDatabase mongoDatabase)
    {
        _transports = mongoDatabase.GetCollection<Transport>("Transports")
            ?? throw new ArgumentNullException(nameof(mongoDatabase));

        InitData();
    }

    public async Task<IEnumerable<Transport>> GetAllAsync()
        => await _transports.FindAsync(_ => true).Result.ToListAsync();

    public async Task<Transport> GetAsync(string name, string type)
        => await _transports.FindAsync(t => t.Name == name && t.Type == type).Result.FirstOrDefaultAsync();

    private void InitData()
    {
        if (_transports.CountDocuments("{}") is not 0)
        {
            return;
        }

        _transports.InsertMany(new List<Transport>()
        {
            new("Kamaz", "Ground", 90, 5.96f, 2.4f, 2.5f),
            new("Fura", "Ground", 80, 13.6f, 2.45f, 3.5f),
            new("Train", "Ground", 90, 14.6f, 3.4f, 3.15f),
            new("Airplane", "Air", 900, 29.5f, 3.2f, 2.8f),
            new("Ship", "Water", 70, 160f, 15f, 28.6f)
        });
    }
}
