using CargoObject.Domain.SeedWork;

namespace CargoObject.Domain.ReadModels.CargoAggregates;

/// <summary>
/// Base aggregate root for reading. 
/// </summary>
public class Cargo : Entity, IAggregateRoot
{
    public string Name { get; private set; }
    public string City { get; private set; }
    public float Price { get; private set; }
    public DateTime Time { get; private set; }
    public float CurrentVolume { get; private set; }
    public CargoType CargoType { get; private set; }
    public List<CargoItem> CargoItems {get; private set;}

    public Cargo()
    {
        CargoItems = new List<CargoItem>();
    }

    public Cargo(Guid id, string name, string city, float money, DateTime time,
        float currentVolume, CargoType cargoType, List<CargoItem> cargoItems)
    {
        Id = id;
        Name = name;
        City = city;
        Price = money;
        Time = time;
        CargoItems = cargoItems;
        CargoType = cargoType;
        CurrentVolume = currentVolume;
    }
}
