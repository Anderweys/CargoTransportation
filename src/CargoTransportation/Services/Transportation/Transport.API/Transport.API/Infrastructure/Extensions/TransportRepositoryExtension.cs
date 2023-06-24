using Transportation.API.Infrastructure.Repositories;
using Transportation.API.Application.Models;

namespace Transportation.API.Infrastructure.Extensions;

public static class TransportRepositoryExtension
{
    public static void AddTransportRepository(this IServiceCollection services)
    {
        services.AddSingleton<ITransportRepository, TransportRepository>();
    }
}
