using CargoObject.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CargoObject.API.Extensions;

public static class AddCargoContextExtention
{
    public static void AddCargoContext(this IServiceCollection services, IConfiguration configuration)
        => services.AddDbContext<CargoContext>(options
            => options.UseSqlServer(configuration.GetConnectionString("CargoMssql")));
}
