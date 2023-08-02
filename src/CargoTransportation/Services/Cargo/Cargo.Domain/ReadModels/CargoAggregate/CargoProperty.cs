using CargoObject.Domain.SeedWork;

namespace CargoObject.Domain.ReadModels.CargoAggregates;

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
}