using MediatR;
using Routing.API.Application.Models;
using Routing.API.Application.Models.DTOs;

namespace Routing.API.Application.Queries;

public class LoadCitiesQueryHandler : IRequestHandler<LoadCitiesQuery, IEnumerable<CityNameDTO>>
{
    private readonly ICityRepository _cityRepository;

    public LoadCitiesQueryHandler(ICityRepository cityRepository)
    {
        _cityRepository = cityRepository ?? throw new ArgumentNullException(nameof(cityRepository));
    }

    public async Task<IEnumerable<CityNameDTO>> Handle(LoadCitiesQuery request, CancellationToken cancellationToken)
    {
        var cities = await _cityRepository.GetAllAsync();

        var cityNames = new List<CityNameDTO>();
        foreach (var city in cities)
        {
            cityNames.Add(new(city.Name));
        }

        return cityNames;
    }
}
