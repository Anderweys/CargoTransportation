using MediatR;
using Routing.API.Application.Models.DTOs;
using System.Runtime.Serialization;

namespace Routing.API.Application.Queries;

[DataContract]
public class GetDeliveryInfoQuery : IRequest<DeliveryInfoDTO>
{
    [DataMember]
    public string City { get; init; }

    [DataMember]
    public string UserId { get; init; }

    public GetDeliveryInfoQuery()
    {
    }

    public GetDeliveryInfoQuery(string city, string userId)
    {
        City = city;
        UserId = userId;
    }
}
