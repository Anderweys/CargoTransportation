using MediatR;
using System.Runtime.Serialization;
using Transportation.API.Models.DTOs;

namespace Transportation.API.Commands.Command;

[DataContract]
public class GetTransportTypeCommand : IRequest<IEnumerable<TransportTypeDTO>>
{ }
