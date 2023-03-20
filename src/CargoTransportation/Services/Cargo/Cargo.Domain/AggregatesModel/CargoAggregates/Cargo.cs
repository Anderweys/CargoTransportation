using CargoObject.Domain.Events;
using CargoObject.Domain.SeedWork;
using System.Collections.ObjectModel;

namespace CargoObject.Domain.AggregatesModel.CargoAggregates;

public class Cargo : Entity, IAggregateRoot
{
    public string UserId { get; private set; }
    public string Name { get; private set; }
    public string City { get; private set; }
    public float Price { get; private set; }
    public DateTime Time { get; private set; }
    public float CurrentVolume { get; private set; }
    public CargoType CargoType { get; private set; }
    private List<CargoItem> _cargoItems;

    public Cargo()
    {
        _cargoItems = new List<CargoItem>();
    }

    public Cargo(string userId, string name, string city, float money, DateTime time, CargoType cargoType)
    {
        UserId = userId;
        Name = name;
        City = city;
        Price = money;
        Time = time;
        CargoType = cargoType;
        CurrentVolume = 0;
        _cargoItems = new List<CargoItem>();
    }

    public void PlaceItem(string name, string description, float price, 
        float length, float height, float width)
    {

        if (HasFreePlace(length, height, width))
        {
            AddItem(new(name, description, price, length, width, height));
        }
    }

    public void RemoveCargoItemById(int id)
    {
        _cargoItems.RemoveAt(id);
    }

    public void LoadCargo()
    {
        var cargoPlacedDomainEvent = new CargoPlacedDomainEvent(
                UserId.Length,
                $"A new cargo with items prepared for loading.\n" +
                $"LoaderId: {UserId}\nId: {Id}",
                this,
                DateTime.UtcNow);

        AddDomainEvent(cargoPlacedDomainEvent);
    }

    public List<CargoItem> CargoItems => _cargoItems.AsReadOnly().ToList();

    private void AddItem(CargoItem item)
    {
        CurrentVolume += item.Length * item.Height * item.Width;
        _cargoItems.Add(item);
    }

    private bool HasFreePlace(float length, float height, float width)
    {
        var volume = length * height * width;
        if (CargoType.CargoSize.Volume < CurrentVolume + volume)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
