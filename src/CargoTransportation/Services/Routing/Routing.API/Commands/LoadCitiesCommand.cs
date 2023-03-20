using MediatR;
using Routing.API.Models.DTOs;
using System.Runtime.Serialization;

namespace Routing.API.Commands;

[DataContract]
public class LoadCitiesCommand : IRequest<IEnumerable<CityNameDTO>>
{
}
