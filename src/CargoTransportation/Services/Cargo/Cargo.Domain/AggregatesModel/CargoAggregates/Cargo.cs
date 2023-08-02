using CargoObject.Domain.Events;
using CargoObject.Domain.SeedWork;

namespace CargoObject.Domain.AggregatesModel.CargoAggregates;

public class Cargo : Aggregate, IAggregateRoot, ICloneable
{
    public string Name { get; private set; }
    public string City { get; private set; }
    public float Price { get; private set; }
    public DateTime Time { get; private set; }
    public float CurrentVolume { get; private set; }
    public CargoType CargoType { get; private set; }
    private List<CargoItem> _cargoItems;
    public List<CargoItem> CargoItems => _cargoItems.AsReadOnly().ToList();

    public Cargo()
    {
    }

    public Cargo(Guid id, string name, string city, float money, DateTime time, CargoType cargoType, List<CargoItem>? cargoItems = default, float currentVolume = 0)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentNullException(nameof(id));
        }

        var @event = new CargoCreatedDomainEvent(
            id,
            name,
            city,
            money,
            time,
            currentVolume,
            cargoType,
            cargoItems?? new()
        );

        ApplyEvent(@event);
    }

    internal void Apply(CargoCreatedDomainEvent @event)
    {
        Id = @event.Id;
        Name = @event.Name;
        City = @event.City;
        Price = @event.Price;
        Time = @event.Time;
        CurrentVolume = @event.CurrentVolume;
        CargoType = @event.CargoType;
        _cargoItems = @event.CargoItems;
    }
    internal void Apply(ItemAddedInCargoDomainEvent @event)
    {
        AddItem(new(
            @event.Name,
            @event.Description,
            @event.Price,
            @event.Length,
            @event.Width,
            @event.Height
        ));
    }

    public void PlaceItem(string name, string description, float price,
        float length, float height, float width)
    {

        if (!HasFreePlace(length, height, width))
        {
            throw new ArgumentOutOfRangeException("The item so large!");
        }

        ApplyEvent(new ItemAddedInCargoDomainEvent(
            name,
            description,
            price,
            length,
            width,
            height
        ));
    }

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

    public object Clone() => new Cargo(Id, Name, City, Price, Time, CargoType, CargoItems, CurrentVolume);
}
