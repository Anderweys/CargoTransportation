using CargoObject.Domain.SeedWork;
using CargoTypeReadModel = CargoObject.Domain.ReadModels.CargoAggregates.CargoType;

namespace CargoObject.Domain.AggregatesModel.CargoAggregates;

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

    public CargoType ParseFromReadModel(CargoTypeReadModel readModel)
    {
        Id = readModel.Id;
        TypeName = readModel.TypeName;
        CargoSize = new CargoSize().ParseFromReadModel(readModel.CargoSize);
        CargoProperty = new CargoProperty().ParseFromReadModel(readModel.CargoProperty);

        return this;
    }
}

