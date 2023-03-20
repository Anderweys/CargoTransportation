using MediatR;
using System.Runtime.Serialization;
using Transportation.API.Models.DTOs;

namespace Transportation.API.Commands.Command;

[DataContract]
public class GetTransportInfoCommand : IRequest<TransportInfoDTO>
{
    [DataMember]
    public string UserId { get; init; }
    [DataMember]
    public string TransportName { get; init; }
    [DataMember]
    public string TransportType { get; init; }

    public GetTransportInfoCommand()
    {
    }

    public GetTransportInfoCommand(string userId, string transportType, string transportName)
    {
        UserId = userId;
        TransportType = transportType;
        TransportName = transportName;
    }
}
