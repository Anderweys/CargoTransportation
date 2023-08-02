using MediatR;
using CargoObject.Application.Models;
using CargoObject.Application.Queries.Query;
using CargoObject.Domain.ReadModels.CargoAggregates;

namespace CargoObject.Application.Queries.Handler;

public class GetCargoInfoQueryHandler : IRequestHandler<GetCargoInfoQuery, IEnumerable<CargoInfo>>
{
    private readonly ICargoReadRepository _cargoRepository;

    public GetCargoInfoQueryHandler(ICargoReadRepository cargoRepository)
    {
        _cargoRepository = cargoRepository ?? throw new ArgumentNullException(nameof(cargoRepository));
    }

    public async Task<IEnumerable<CargoInfo>> Handle(GetCargoInfoQuery request, CancellationToken cancellationToken)
    {
        var cargos = await _cargoRepository.GetByUserIdAsync(request.UserId);

        if (cargos is null)
        {
            return new List<CargoInfo>();
        }

        List<CargoInfo> cargoInfos = new();
        foreach (var cargo in cargos)
        {
            List<Item> items = new();
            foreach (var item in cargo.CargoItems)
            {
                items.Add(new(
                    item.Id,
                    item.Name,
                    item.Description,
                    item.Length,
                    item.Width,
                    item.Height,
                    item.Price));
            }
            cargoInfos.Add(new(
                cargo.City, items, cargo.Time, cargo.Price));
        }

        return cargoInfos;
    }
}
