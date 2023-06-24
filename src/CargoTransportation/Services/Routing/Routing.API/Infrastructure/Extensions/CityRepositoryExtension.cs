using Routing.API.Infrastructure.Repositories;
using Routing.API.Application.Models;

namespace Routing.API.Infrastructure.Extensions;

public static class CityRepositoryExtension
{
    public static void AddCityRepository(this IServiceCollection services)
        => services.AddSingleton<ICityRepository, CityRepository>();
}
