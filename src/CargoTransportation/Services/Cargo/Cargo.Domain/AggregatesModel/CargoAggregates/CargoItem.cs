using CargoObject.Domain.SeedWork;

namespace CargoObject.Domain.AggregatesModel.CargoAggregates;


public class CargoItem : Entity
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public float Volume { get; private set; }
    public float Price { get; private set; }

    public CargoItem()
    {
    }

    public CargoItem(string name, string description, float volume, float price)
    {
        Name=name;
        Description=description;
        Volume=volume;
        Price=price;
    }
}
