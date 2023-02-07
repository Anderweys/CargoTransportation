namespace Cargo.Domain.SeedWork;


/// <summary>
/// The base value object.
/// </summary>
public abstract class ValueObject
{
    public ValueObject GetCopy() => MemberwiseClone() as ValueObject;
}
