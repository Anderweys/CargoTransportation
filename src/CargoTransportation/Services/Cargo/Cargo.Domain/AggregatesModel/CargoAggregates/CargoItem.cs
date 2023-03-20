using CargoObject.Domain.SeedWork;

namespace CargoObject.Domain.AggregatesModel.CargoAggregates;

public class CargoItem : Entity
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public float Length { get; private set; }
    public float Width { get; private set; }
    public float Height { get; private set; }
    public float Price { get; private set; }

    public CargoItem()
    {
    }

    public CargoItem(string name, string description, float price, float length, float width, float height)
    {
        Name=name;
        Description=description;
        Length=length;
        Width=width;
        Height=height;
        Price=price;
    }
}
