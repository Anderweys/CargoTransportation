using WebMVC.Services;

namespace WebMVC.Extensions;

public static class ControllerWebServiceExtension
{
    public static void AddControllerWebServices(this IServiceCollection services)
    {
        services.AddSingleton<ICargoService, CargoService>();
        services.AddSingleton<ITransportService, TransportService>();
        services.AddSingleton<IRoutingService, RoutingService>();
    }
}
