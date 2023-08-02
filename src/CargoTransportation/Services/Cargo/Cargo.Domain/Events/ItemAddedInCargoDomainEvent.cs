namespace CargoObject.Domain.Events;

public record ItemAddedInCargoDomainEvent(
    string Name,
    string Description,
    float Price,
    float Length,
    float Width,
    float Height
);
