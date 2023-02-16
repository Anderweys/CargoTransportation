using CargoObject.Domain.SeedWork;

namespace CargoObject.Domain.AggregatesModel.CargoAggregates;

public class CargoSize : ValueObject
{
    public float Length { get; private set; }
    public float Width { get; private set; }
    public float Height { get; private set; }
    public float Volume { get; private set; }

    public CargoSize()
    {
    }

    public CargoSize(float length, float width, float heigth, float volume)
    {
        Length = length;
        Width = width;
        Height = heigth;
        Volume = volume;
    }
}
