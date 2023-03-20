using CargoObject.Domain.AggregatesModel.CargoAggregates;
using CargoObject.Infrastructure.Repositories;

namespace CargoObject.API.Extensions;

public static class CargoRepositoryExtension
{
    public static void AddCargoRepository(this IServiceCollection services)
    {
        services.AddScoped<ICargoRepository, CargoRepository>();
    }
}
