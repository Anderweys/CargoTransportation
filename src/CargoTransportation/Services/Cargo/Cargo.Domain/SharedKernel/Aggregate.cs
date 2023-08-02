using MediatR;
using System.Text.Json.Serialization;

namespace CargoObject.Domain.SeedWork;

/// <summary>
/// The base Aggregate.
/// </summary>
public abstract class Aggregate
{
    public Guid Id { get; protected set; }
    public long Version { get; protected set; }

    [JsonIgnore] 
    private readonly List<object> _uncommittedEvents = new();

    public IEnumerable<object> GetUncommittedEvents()
    {
        return _uncommittedEvents;
    }

    public void ClearUncommittedEvents()
    {
        _uncommittedEvents.Clear();
    }

    protected void AddUncommittedEvent(object @event)
    {
        _uncommittedEvents.Add(@event);
    }

    protected void ApplyEvent(object @event)
    {
        /*  Analog:
         *  
         *  dynamic projection = this;
         *  dynamic e = @event;
         *  projection.Apply(e);
         */

        ((dynamic)this).Apply((dynamic)@event);
        _uncommittedEvents.Add(@event);

        Version++;
    }
}
