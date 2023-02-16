using CargoObject.Domain.AggregatesModel.CargoAggregates;
using MediatR;

namespace CargoObject.Domain.Events;

/// <summary>
/// It's when a cargo prepared for loading.
/// </summary>
public class CargoPlacedDomainEvent : INotification
{
    public int LoaderId { get; }
    public string Description { get; }
    public Cargo Cargo { get; }
    public DateTime LoadData { get; }

    public CargoPlacedDomainEvent(int loaderId, string description, Cargo cargo, DateTime loadData)
    {
        LoaderId = loaderId;
        Description=description;
        Cargo=cargo;
        LoadData=loadData;
    }
}

