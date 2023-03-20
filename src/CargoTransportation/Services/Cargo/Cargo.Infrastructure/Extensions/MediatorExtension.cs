using MediatR;
using CargoObject.Domain.SeedWork;

namespace CargoObject.Infrastructure.Extensions;

/// <summary>
/// Extension for publish domain events.
/// </summary>
public static class MediatorExtension
{
    public static async Task ExecuteDomainEventsAsync(this IMediator mediator, CargoContext context)
    {
        // P.S. On project no Domain events. So sad...
        //var domainEntities = context.ChangeTracker
        //    .Entries<Entity>()
        //    .Where(e => e.Entity.DomainEvents is not null && e.Entity.DomainEvents.Any());

        //foreach (var entity in domainEntities)
        //{
        //    await mediator.Publish(entity);
        //    entity.Entity.ClearDomainEvent();
        //}
    }
}
