using CargoObject.Domain.SeedWork;

namespace CargoObject.Domain.AggregatesModel.CargoAggregates;


public class CargoType : ValueObject
{
    public string TypeName { get; private set; }
    public CargoSize CargoSize { get; private set; }
    public CargoProperty CargoProperty { get; private set; }


    public CargoType()
    {
    }

    public CargoType(string typeName, CargoSize cargoSize, CargoProperty cargoProperty)
    {
        TypeName = typeName;
        CargoSize = cargoSize;
        CargoProperty = cargoProperty;
    }
}

