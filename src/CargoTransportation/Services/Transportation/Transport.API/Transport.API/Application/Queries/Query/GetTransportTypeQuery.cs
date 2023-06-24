using MediatR;
using System.Runtime.Serialization;
using Transportation.API.Application.Models.DTOs;

namespace Transportation.API.Application.Queries.Query;

[DataContract]
public class GetTransportTypeQuery : IRequest<IEnumerable<TransportTypeDTO>>
{ }
