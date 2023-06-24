using MediatR;
using Routing.API.Application.Models.DTOs;
using System.Runtime.Serialization;

namespace Routing.API.Application.Queries;

[DataContract]
public class LoadCitiesQuery : IRequest<IEnumerable<CityNameDTO>>
{
}
