using MediatR;

namespace Cargo.Domain.SeedWork;


/// <summary>
/// The base entity.
/// </summary>
public abstract class Entity
{
    /// <summary>
    /// The identifier.
    /// Is a important entity's member.
    /// </summary>
    private int _id;

    public virtual int Id
    {
        get => Id;
        protected set
        {
            _id = value;
        }
    }

    /// <summary>
    /// The domain events.
    /// Using for saving domain events from other aggregates.
    /// </summary>
    private List<INotification> _domainEvents;
    public IReadOnlyCollection<INotification> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(INotification domainEvent)
    {
        _domainEvents ??= new List<INotification>();
        _domainEvents.Add(domainEvent);
    }
    public void RemoveDomainEvent(INotification domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }
    public void ClearDomainEvent()
    {
        _domainEvents.Clear();
    }
}
