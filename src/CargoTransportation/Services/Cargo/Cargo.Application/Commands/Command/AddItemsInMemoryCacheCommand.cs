using MediatR;
using CargoObject.Application.Models;
using System.Runtime.Serialization;

namespace CargoObject.Application.Commands.Command;

[DataContract]
public class AddItemsInMemoryCacheCommand : IRequest<bool>
{
    [DataMember]
    public string UserId { get; init; }
    [DataMember]
    public IEnumerable<Item> Items { get; init; }

    public AddItemsInMemoryCacheCommand(string userId, IEnumerable<Item> items)
    {
        UserId = userId;
        Items = items;
    }
}
