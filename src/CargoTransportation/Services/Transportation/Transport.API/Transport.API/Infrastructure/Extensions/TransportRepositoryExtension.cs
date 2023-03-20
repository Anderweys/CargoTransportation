using Transportation.API.Models;
using Transportation.API.Infrastructure.Repositories;

namespace Transportation.API.Infrastructure.Extensions;

public static class TransportRepositoryExtension
{
    public static void AddTransportRepository(this IServiceCollection services)
    {
        services.AddSingleton<ITransportRepository, TransportRepository>();
    }
}
