using MediatR;
using CargoObject.Application.Models;

namespace CargoObject.Application.Commands.Command;

public record CreateCargoCommand(
    string UserId,
    string City,
    DateTime Time,
    float Money,
    IEnumerable<Item> Items
    ) : IRequest<bool>;
