using CargoObject.Domain.AggregatesModel.CargoAggregates;
using CargoObject.Domain.ReadModels.CargoAggregates;
using CargoObject.Infrastructure.Repositories;

namespace CargoObject.API.Extensions;

public static class CargoRepositoryExtension
{
    public static void AddCargoRepository(this IServiceCollection services)
    {
        services.AddScoped<ICargoWriteRepository, CargoWriteRepository>();
        services.AddScoped<ICargoReadRepository, CargoReadRepository>();
    }
}
