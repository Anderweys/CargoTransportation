using MediatR;
using System.Runtime.Serialization;
using Transportation.API.Models.DTOs;

namespace Transportation.API.Commands.Command;

[DataContract]
public class GetUserItemsCommand : IRequest<IEnumerable<ItemDTO>>
{
    [DataMember]
    public string UserId { get; set; }

    public GetUserItemsCommand()
    {
    }

    public GetUserItemsCommand(string userId)
    {
        UserId = userId;
    }
}
