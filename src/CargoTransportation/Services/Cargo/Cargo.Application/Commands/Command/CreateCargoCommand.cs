using MediatR;
using CargoObject.Application.Models;

namespace CargoObject.Application.Commands.Command;

public class CreateCargoCommand : IRequest<bool>
{
    public string UserId { get; init; }
    public string City { get; init; }
    public DateTime Time { get; init; }
    public float Money { get; init; }
    public IEnumerable<Item> Items { get; init; }

    public CreateCargoCommand(string userId, string city, DateTime time, float money, IEnumerable<Item> items)
    {
        UserId=userId;
        City=city;
        Time=time;
        Money=money;
        Items=items;
    }
}
