using MediatR;
using Routing.API.Models;
using Routing.API.Models.DTOs;

namespace Routing.API.Commands;

public class LoadCitiesCommandHandler : IRequestHandler<LoadCitiesCommand, IEnumerable<CityNameDTO>>
{
    private readonly ICityRepository _cityRepository;

    public LoadCitiesCommandHandler(ICityRepository cityRepository)
    {
        _cityRepository=cityRepository ?? throw new ArgumentNullException(nameof(cityRepository));
    }

    public async Task<IEnumerable<CityNameDTO>> Handle(LoadCitiesCommand request, CancellationToken cancellationToken)
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
