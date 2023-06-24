using Account.API.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Account.API.Extensions;

public static class AccountContextExtention
{
    public static void AddAccountContext(this IServiceCollection services, IConfiguration configuration)
        => services.AddDbContext<AccountContext>(options
            => options.UseNpgsql(configuration
                .GetConnectionString("AccountPostgres")));
}
