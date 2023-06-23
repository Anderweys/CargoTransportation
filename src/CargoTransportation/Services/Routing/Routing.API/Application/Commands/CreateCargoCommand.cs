using MediatR;
using System.Runtime.Serialization;

namespace Routing.API.Application.Commands;

[DataContract]
public class CreateCargoCommand : IRequest<bool>
{
    [DataMember]
    public string UserId { get; set; }
    [DataMember]
    public string City { get; set; }
    [DataMember]
    public DateTime Time { get; set; }
    [DataMember]
    public float Money { get; set; }

    public CreateCargoCommand(string userId, float money, DateTime time, string city)
    {
        UserId = userId;
        Money = money;
        Time = time;
        City = city;
    }
}
