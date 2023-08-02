using CargoObject.Domain.SeedWork;
using CargoPropertyReadModel = CargoObject.Domain.ReadModels.CargoAggregates.CargoProperty;

namespace CargoObject.Domain.AggregatesModel.CargoAggregates;

public class CargoProperty : ValueObject
{
    public float MaxTemperature { get; private set; }
    public float MinTemperature { get; private set; }
    public float MaxPressure { get; private set; }
    public float MinPressure { get; private set; }

    public CargoProperty()
    {
    }

    public CargoProperty(float maxTemperature, float minTemperature,
        float maxPressure, float minPressure)
    {
        MaxTemperature=maxTemperature;
        MinTemperature=minTemperature;
        MaxPressure=maxPressure;
        MinPressure=minPressure;
    }

    public CargoProperty ParseFromReadModel(CargoPropertyReadModel readModel)
    {
        MaxTemperature=readModel.MaxTemperature;
        MinTemperature=readModel.MinTemperature;
        MaxPressure=readModel.MaxPressure;
        MinPressure=readModel.MinPressure;
        return this;
    }
}