using MediatR;
using Microsoft.Extensions.Logging;
using CargoObject.Application.Commands.Command;
using CargoObject.Domain.ReadModels.CargoAggregates;
using CargoObject.Domain.AggregatesModel.CargoAggregates;
using CargoAggregate = CargoObject.Domain.AggregatesModel.CargoAggregates.Cargo;
using CargoReadModel = CargoObject.Domain.ReadModels.CargoAggregates.Cargo;
using CargoItemReadModel = CargoObject.Domain.ReadModels.CargoAggregates.CargoItem;
using CargoType = CargoObject.Domain.AggregatesModel.CargoAggregates.CargoType;

namespace CargoObject.Application.Commands.Handler;

public class CreateCargoCommandHandler : IRequestHandler<CreateCargoCommand, bool>
{
    private readonly ICargoReadRepository _cargoReader;
    private readonly ICargoWriteRepository _cargoWriter;
    private readonly ILogger<CreateCargoCommandHandler> _logger;

    public CreateCargoCommandHandler(
        ICargoReadRepository cargoReader,
        ICargoWriteRepository cargoWriter,
        ILogger<CreateCargoCommandHandler> logger)
    {
        _cargoReader = cargoReader ?? throw new ArgumentNullException(nameof(cargoReader));
        _cargoWriter=cargoWriter ?? throw new ArgumentNullException(nameof(cargoWriter));
        _logger=logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<bool> Handle(CreateCargoCommand request, CancellationToken cancellationToken)
    {
        var itemsVolume = request.Items.Sum(i => i.Length * i.Height * i.Width);
        try
        {
            var cargoTypeReadModel = await _cargoReader.GetCargoTypeAsync(itemsVolume);
            var cargoTypeAggregate = new CargoType().ParseFromReadModel(cargoTypeReadModel);
            var cargoAggregate = new CargoAggregate(Guid.NewGuid(), request.UserId, request.City, request.Money, request.Time, cargoTypeAggregate);

            foreach (var item in request.Items)
            {
                cargoAggregate.PlaceItem(
                    item.Name,
                    item.Description,
                    item.Price,
                    item.Length,
                    item.Width,
                    item.Height);
            }

            var cargo = cargoAggregate.Clone() as CargoAggregate;
            var isAdded = await _cargoWriter.AddAsync(cargoAggregate);

            if (!isAdded)
            {
                throw new ArgumentNullException();
            }

            List<CargoItemReadModel> cargoItemReadModel = new();
            foreach (var item in cargo.CargoItems)
            {
                cargoItemReadModel.Add(new(
                    item.Name,
                    item.Description,
                    item.Price,
                    item.Length,
                    item.Width,
                    item.Height)
                );
            }

            CargoReadModel cargoRead = new(
                id: cargo.Id,
                name: cargo.Name,
                city: cargo.City,
                money: request.Money,
                time: request.Time,
                currentVolume: cargo.CurrentVolume,
                cargoType: cargoTypeReadModel,
                cargoItems: cargoItemReadModel);

             await _cargoReader.AddAsync(cargoRead);
        }
        catch (ArgumentNullException e)
        {
            _logger.LogCritical(
                "Cargo wasn't been added. \n\tCommand handler: {handler},\n\tException: {e}, message: {m}, stack trace: {st},\n\t Time: {time}.",
                nameof(ArgumentNullException),
                e.Message,
                e.StackTrace,
                nameof(CreateCargoCommandHandler),
                DateTime.UtcNow.ToLongDateString());

            return false;
        }

        _logger.LogInformation(
            "Cargo has been added. Command handler: {handler}, time: {time}.",
            nameof(CreateCargoCommandHandler),
            DateTime.UtcNow.ToLongDateString());

        return true;
    }
}
