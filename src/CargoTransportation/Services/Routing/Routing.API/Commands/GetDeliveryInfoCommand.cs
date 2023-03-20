using MediatR;
using Routing.API.Models.DTOs;
using System.Runtime.Serialization;

namespace Routing.API.Commands;

[DataContract]
public class GetDeliveryInfoCommand : IRequest<DeliveryInfoDTO>
{
    [DataMember]
    public string City { get; init; }

    [DataMember]
    public string UserId { get; init; }

    public GetDeliveryInfoCommand()
    {
    }

    public GetDeliveryInfoCommand(string city, string userId)
    {
        City = city;
        UserId = userId;
    }
}
