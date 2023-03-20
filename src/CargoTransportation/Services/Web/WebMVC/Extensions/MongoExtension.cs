using MongoDB.Driver;

namespace WebMVC.Extensions;

public static class MongoExtension
{
    public static void AddMongo(this IServiceCollection services, IConfiguration configuration)
        => services.AddSingleton(new MongoClient(
            configuration.GetSection("ConnectionString").Value)
            .GetDatabase(configuration.GetSection("DatabaseName").Value));
}