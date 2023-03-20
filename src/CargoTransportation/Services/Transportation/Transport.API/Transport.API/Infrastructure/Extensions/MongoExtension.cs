using MongoDB.Driver;

namespace Transportation.API.Infrastructure.Extensions;

public static class MongoExtension
{
    public static void AddMongo(this IServiceCollection services, IConfiguration configuration)
        => services.AddSingleton(new MongoClient(
            configuration.GetSection("ConnectionString").Value)
            .GetDatabase(configuration.GetSection("DatabaseName").Value));
}
