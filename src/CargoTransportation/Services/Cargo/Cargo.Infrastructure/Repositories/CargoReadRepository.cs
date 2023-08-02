using Microsoft.EntityFrameworkCore;
using CargoObject.Domain.ReadModels.CargoAggregates;

namespace CargoObject.Infrastructure.Repositories;

public class CargoReadRepository : ICargoReadRepository
{
    private readonly CargoContext _context;

    public CargoReadRepository(CargoContext context)
    {
        _context = context;
    }

    public async Task<bool> AddAsync(Cargo cargo)
    {
        var result = await _context.Cargos.AddAsync(cargo) is not null;

        await _context.SaveChangesAsync();

        return result;
    }

    public async Task<IEnumerable<Cargo>> GetByUserIdAsync(string id)
    {
        var result = await _context.Cargos
            .Include(c => c.CargoItems)
            .Where(c => c.Name == id)
            .ToListAsync();

        if (result is null)
        {
            return Enumerable.Empty<Cargo>();
        }

        return result;
    }

    public async Task<CargoType> GetCargoTypeAsync(float itemsVolume)
    {
        IEnumerable<CargoType> cargoTypes = await _context.CargoTypes
            .Include(c => c.CargoSize)
            .Include(c => c.CargoProperty)
            .ToListAsync();

        var result = new CargoType();

        foreach (var cargoType in cargoTypes)
        {
            if (cargoType.CargoSize.Volume > itemsVolume)
            {
                result = cargoType; break;
            }
        }

        return result;
    }
}
