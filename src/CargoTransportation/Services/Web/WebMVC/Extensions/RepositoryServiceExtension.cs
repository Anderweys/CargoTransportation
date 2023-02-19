using WebMVC.Infrastructure.Repository;
using WebMVC.Models;

namespace WebMVC.Extensions;

public static class RepositoryServiceExtension
{
    public static IServiceCollection AddRepositoryService(this IServiceCollection services)
    {
        services.AddSingleton<IUserRepository, UserRepository>();

        return services;
    }
}

