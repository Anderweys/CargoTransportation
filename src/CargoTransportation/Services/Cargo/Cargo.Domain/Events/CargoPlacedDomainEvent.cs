using CargoObject = Cargo.Domain.AggregatesModel.CargoAggregates.Cargo;
using MediatR;

namespace Cargo.Domain.Events;

/// <summary>
/// It's when a cargo prepared for loading.
/// </summary>
public class CargoPlacedDomainEvent : INotification
{
    public int LoaderId { get; }
    public string Description { get; }
    public CargoObject Cargo { get; }
    public DateTime LoadData { get; }

    public CargoPlacedDomainEvent(int loaderId, string description, CargoObject cargo, DateTime loadData)
    {
        LoaderId = loaderId;
        Description=description;
        Cargo=cargo;
        LoadData=loadData;
    }
}

