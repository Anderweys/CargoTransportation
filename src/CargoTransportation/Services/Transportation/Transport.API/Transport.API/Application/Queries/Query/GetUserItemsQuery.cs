using MediatR;
using System.Runtime.Serialization;
using Transportation.API.Application.Models.DTOs;

namespace Transportation.API.Application.Queries.Query;

[DataContract]
public class GetUserItemsQuery : IRequest<IEnumerable<ItemDTO>>
{
    [DataMember]
    public string UserId { get; set; }

    public GetUserItemsQuery()
    {
    }

    public GetUserItemsQuery(string userId)
    {
        UserId = userId;
    }
}
