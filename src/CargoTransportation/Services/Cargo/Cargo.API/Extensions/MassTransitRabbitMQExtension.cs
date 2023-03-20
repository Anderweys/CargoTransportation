using MassTransit;
using CargoObject.Application.IntegrationEvents.Handlers;

namespace CargoObject.API.Extensions;

public static class MassTransitRabbitMQExtension
{
    public static void AddMassTransitRabbitMQ(this IServiceCollection services, IConfiguration congif)
    {
        services.AddMassTransit(c =>
        {
            c.AddConsumer<CreateCargoIntegrationEventHandler>();

            c.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(congif.GetSection("ConnectionString").Value, host =>
                {
                    host.Username(congif.GetSection("Username").Value);
                    host.Password(congif.GetSection("Password").Value);
                });

                var ep = congif.GetSection("MicroservicesEndpoints").GetSection("Cargo");

                cfg.ReceiveEndpoint(ep.GetSection("Handler").GetSection("Name").Value, h =>
                {
                    h.ConfigureConsumeTopology = false;

                    h.ConfigureConsumer<CreateCargoIntegrationEventHandler>(ctx);

                    h.Bind(ep.GetSection("Publisher").GetSection("Name").Value);
                });
            });
        });
    }
}
