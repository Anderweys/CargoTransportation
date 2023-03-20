using Routing.API.Models;
using Routing.API.Infrastructure.Repositories;

namespace Routing.API.Infrastructure.Extensions;

public static class CityRepositoryExtension
{
    public static void AddCityRepository(this IServiceCollection services)
        => services.AddSingleton<ICityRepository, CityRepository>();
}
