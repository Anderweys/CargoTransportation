using Transportation.API.Services;

namespace Transportation.API.Infrastructure.Extensions;

public static class CalculationPriceServiceExtension
{
    public static void AddCalculationPrice(this IServiceCollection services)
    {
        services.AddSingleton<CalculationPriceService>();
    }
}
