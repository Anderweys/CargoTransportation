using Microsoft.EntityFrameworkCore;
using CargoObject.Domain.AggregatesModel.CargoAggregates;

namespace CargoObject.Infrastructure.Repositories;

public class CargoRepository : ICargoRepository
{
    private readonly CargoContext _context;

    public CargoRepository(CargoContext context)
    {
        _context = context;
    }

    public async Task<Cargo> AddAsync(Cargo cargo)
    {
        var result = await _context.Cargos.AddAsync(cargo);

        if (result.Entity is null)
        {

        }

        await _context.SaveChangesAsync();

        return result.Entity;
    }

    public async Task<IEnumerable<Cargo>> GetByUserIdAsync(string userId)
    {
        var result = await _context.Cargos
            .Include(c => c.CargoItems)
            .Where(c => c.UserId == userId)
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
