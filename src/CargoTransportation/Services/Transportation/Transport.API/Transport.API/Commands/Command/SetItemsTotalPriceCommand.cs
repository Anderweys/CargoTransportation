using MediatR;
using System.Runtime.Serialization;
using Transportation.API.Models.DTOs;

namespace Transportation.API.Commands.Command;

[DataContract]
public class SetItemsTotalPriceCommand : IRequest<bool>
{
    [DataMember]
    public string UserId { get; init; }

    [DataMember]
    public float TotalPrice { get; init; }

    [DataMember]
    public TransportOptionDTO Option { get; init; }

    public SetItemsTotalPriceCommand()
    {
    }

    public SetItemsTotalPriceCommand(float totalPrice, string userId, string type, float speed)
    {
        TotalPrice = totalPrice;
        UserId = userId;
        Option = new(type, speed);
    }
}
