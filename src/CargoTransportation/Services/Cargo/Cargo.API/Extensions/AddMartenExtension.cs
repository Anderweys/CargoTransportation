using CargoObject.Domain.AggregatesModel.CargoAggregates;
using Microsoft.EntityFrameworkCore;
using Marten;

namespace CargoObject.API.Extensions;

public static class AddMartenExtension
{
    public static void AddMarten(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMarten(opt =>
        {
            opt.Connection(configuration.GetConnectionString("EventStore"));
            opt.Projections.Snapshot<Cargo>(Marten.Events.Projections.SnapshotLifecycle.Inline);
        });
    }
}
