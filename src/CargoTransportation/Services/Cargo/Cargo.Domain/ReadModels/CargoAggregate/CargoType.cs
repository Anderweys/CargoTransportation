using CargoObject.Domain.SeedWork;

namespace CargoObject.Domain.ReadModels.CargoAggregates;

public class CargoType : Entity
{
    public string TypeName { get; private set; }
    public CargoSize CargoSize { get; private set; }
    public CargoProperty CargoProperty { get; private set; }

    public CargoType()
    {
    }

    public CargoType(Guid id, string typeName)
    {
        Id = id;
        TypeName = typeName;
    }
}

