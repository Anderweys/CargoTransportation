using Account.API.Domain.Interface;
using Account.API.Infrastructure;

namespace Account.API.Extensions;

public static class AccountRepositoryExtension
{
    public static void AddAccountRepository(this IServiceCollection services)
        => services.AddScoped<IAccountRepository, AccountRepository>();
}
