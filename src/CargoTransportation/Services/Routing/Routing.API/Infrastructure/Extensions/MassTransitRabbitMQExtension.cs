using MassTransit;
using Contracts.IntegrationEvents;

namespace Routing.API.Infrastructure.Extensions;

public static class MassTransitRabbitMQExtension
{
    public static void AddMassTransitRabbitMQ(this IServiceCollection services, IConfiguration congif)
    {
        services.AddMassTransit(c =>
        {
            c.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(congif.GetSection("ConnectionString").Value, host =>
                {
                    host.Username(congif.GetSection("Username").Value);
                    host.Password(congif.GetSection("Password").Value);
                });

                var ep = congif.GetSection("MicroservicesEndpoints").GetSection("Routing");

                cfg.Message<CreateCargoIntegrationEvent>(m =>
                {
                    m.SetEntityName(ep.GetSection("Publisher").GetSection("Name").Value);
                });
            });
        });
    }
}

