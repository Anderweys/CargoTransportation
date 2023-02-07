using Cargo.Domain.AggregatesModel.LoaderAggragates;
using Cargo.Domain.Events;
using Cargo.Domain.SeedWork;
using System.Collections.ObjectModel;

namespace Cargo.Domain.AggregatesModel.CargoAggregates;


public class Cargo : Entity, IAggregateRoot
{
    public int LoaderId { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public float CurrentVolume { get; private set; }
    public CargoType CargoType { get; private set; }
    private List<CargoItem> _cargoItems;

    public Cargo()
    {
        _cargoItems= new List<CargoItem>();
    }

    public Cargo(int id,                // Cargo.
                 int loaderId,
                 string name,
                 string description,
                 string cargoTypeName,  // CargoType.
                 float size,            // CargoSize.
                 float length,
                 float width,
                 float height,
                 float maxTemperature,  // CargoProprety.
                 float minTemperature,
                 float maxPressure,
                 float minPressure)
    {
        Id = id;
        LoaderId = loaderId;
        Name = name;
        Description = description;
        CargoType = new(
            cargoTypeName,
            new CargoSize(size, length, width, height),
            new CargoProperty(maxTemperature, minTemperature, maxPressure, minPressure)
            );

        CurrentVolume = 0;
        _cargoItems = new List<CargoItem>();
    }

    public void PlaceCargoItem(CargoItem item)
    {
        if (HasFreePlace(item))
        {
            AddItem(item);
        }
    }
    public void RemoveCargoItemById(int id)
    {
        _cargoItems.RemoveAt(id);
    }
    public void LoadCargo()
    {
        var cargoPlacedDomainEvent = new CargoPlacedDomainEvent(
                LoaderId,
                $"A new cargo with items prepared for loading.\n" +
                $"LoaderId: {LoaderId}\nId: {Id}",
                this,
                DateTime.UtcNow);

        AddDomainEvent(cargoPlacedDomainEvent);
    }

    public ReadOnlyCollection<CargoItem> CargoItems() => _cargoItems.AsReadOnly();

    private void AddItem(CargoItem item)
    {
        CurrentVolume += item.Volume;
        _cargoItems.Add(item);
    }
    private bool HasFreePlace(CargoItem item)
    {
        if (CargoType.CargoSize.Volume < CurrentVolume + item.Volume)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
