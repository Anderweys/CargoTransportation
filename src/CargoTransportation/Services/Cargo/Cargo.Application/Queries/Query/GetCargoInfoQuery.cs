using MediatR;
using CargoObject.Application.Models;
using System.Runtime.Serialization;

namespace CargoObject.Application.Queries.Query;

[DataContract]
public class GetCargoInfoQuery : IRequest<IEnumerable<CargoInfo>>
{
    [DataMember]
    public string UserId { get; init; }

    public GetCargoInfoQuery()
    {
    }

    public GetCargoInfoQuery(string userId)
    {
        UserId = userId;
    }
}