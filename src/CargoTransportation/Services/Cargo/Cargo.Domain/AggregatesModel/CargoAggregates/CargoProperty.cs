using CargoObject.Domain.SeedWork;

namespace CargoObject.Domain.AggregatesModel.CargoAggregates;


public class CargoProperty : ValueObject
{
    public LimitValue Temperature { get; private set; }
    public LimitValue Pressure { get; private set; }

    public CargoProperty()
    {
    }

    public CargoProperty(float maxTemperature, float minTemperature,
        float minPressure, float maxPressure)
    {
        Temperature = new() { Max = maxTemperature, Min = minTemperature };
        Pressure = new() { Max = maxPressure, Min = minPressure };
    }
}


/// <summary>
/// Using for min and max value for different cargo's properties.
/// </summary>
public struct LimitValue
{
    public float Min { get; set; }
    public float Max { get; set; }

    public LimitValue()
    {
        Min = 0;
        Max = 0;
    }
}