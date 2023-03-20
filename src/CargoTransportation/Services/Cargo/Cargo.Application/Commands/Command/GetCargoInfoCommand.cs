using MediatR;
using CargoObject.Application.Models;
using System.Runtime.Serialization;

namespace CargoObject.Application.Commands.Command;

[DataContract]
public class GetCargoInfoCommand : IRequest<IEnumerable<CargoInfo>>
{
    [DataMember]
    public string UserId { get; init; }

    public GetCargoInfoCommand()
    {
    }

    public GetCargoInfoCommand(string userId)
    {
        UserId = userId;
    }
}
