using MediatR;

namespace CargoObject.Domain.SeedWork;

/// <summary>
/// The base entity.
/// </summary>
public abstract class Entity
{
    /// <summary>
    /// The identifier.
    /// Is a important entity's member.
    /// </summary>
    private Guid _id;

    public virtual Guid Id
    {
        get => _id;
        protected set
        {
            _id = value;
        }
    }
}
