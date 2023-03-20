using MediatR;
using CargoObject.Application.Models;
using CargoObject.Application.Commands.Command;
using CargoObject.Domain.AggregatesModel.CargoAggregates;

namespace CargoObject.Application.Commands.Handler;

public class GetCargoInfoCommandHandler : IRequestHandler<GetCargoInfoCommand, IEnumerable<CargoInfo>>
{
    private readonly ICargoRepository _cargoRepository;

    public GetCargoInfoCommandHandler(ICargoRepository cargoRepository)
    {
        _cargoRepository=cargoRepository ?? throw new ArgumentNullException(nameof(cargoRepository));
    }

    public async Task<IEnumerable<CargoInfo>> Handle(GetCargoInfoCommand request, CancellationToken cancellationToken)
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
