using MediatR;
using System.Runtime.Serialization;
using Transportation.API.Application.Models.DTOs;

namespace Transportation.API.Application.Queries.Query;

[DataContract]
public class GetTransportInfoQuery : IRequest<TransportInfoDTO>
{
    [DataMember]
    public string UserId { get; init; }
    [DataMember]
    public string TransportName { get; init; }
    [DataMember]
    public string TransportType { get; init; }

    public GetTransportInfoQuery()
    {
    }

    public GetTransportInfoQuery(string userId, string transportType, string transportName)
    {
        UserId = userId;
        TransportType = transportType;
        TransportName = transportName;
    }
}
