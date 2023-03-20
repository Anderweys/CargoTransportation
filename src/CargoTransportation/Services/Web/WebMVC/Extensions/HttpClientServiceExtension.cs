using WebMVC.Services;

namespace WebMVC.Extensions;

public static class HttpClientServiceExtension
{
    public static IServiceCollection AddHttpClientServices(this IServiceCollection services)
    {
        services.AddHttpClient<ICargoService, CargoService>();
        services.AddHttpClient<ITransportService, TransportService>();
        services.AddHttpClient<IRoutingService, RoutingService>();

        return services;
    }
}
