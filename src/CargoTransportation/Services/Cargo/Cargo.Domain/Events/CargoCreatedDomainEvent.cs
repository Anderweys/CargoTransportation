using CargoObject.Domain.AggregatesModel.CargoAggregates;

namespace CargoObject.Domain.Events;

public record CargoCreatedDomainEvent(
    Guid Id,
    string Name,
    string City,
    float Price,
    DateTime Time,
    float CurrentVolume,
    CargoType CargoType,
    List<CargoItem> CargoItems
);