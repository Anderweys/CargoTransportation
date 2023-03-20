using MediatR;
using Microsoft.Extensions.Logging;
using CargoObject.Application.Commands.Command;
using CargoObject.Domain.AggregatesModel.CargoAggregates;

namespace CargoObject.Application.Commands.Handler;

public class CreateCargoCommandHandler : IRequestHandler<CreateCargoCommand, bool>
{
    private readonly ICargoRepository _cargoRepository;
    private readonly ILogger<CreateCargoCommandHandler> _logger;

    public CreateCargoCommandHandler(ICargoRepository cargoRepository, ILogger<CreateCargoCommandHandler> logger)
    {
        _cargoRepository=cargoRepository ?? throw new ArgumentNullException(nameof(cargoRepository));
        _logger=logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<bool> Handle(CreateCargoCommand request, CancellationToken cancellationToken)
    {
        var itemsVolume = request.Items.Sum(i => i.Length * i.Height * i.Width);
        Cargo result;
        try
        {
            var cargoType = await _cargoRepository.GetCargoTypeAsync(itemsVolume);
            var cargo = new Cargo(request.UserId, Guid.NewGuid().ToString(), request.City, request.Money, request.Time, cargoType);

            foreach (var item in request.Items)
            {
                cargo.PlaceItem(
                    item.Name,
                    item.Description,
                    item.Price,
                    item.Length,
                    item.Width,
                    item.Height);
            }

            result = await _cargoRepository.AddAsync(cargo);

            if (result is null)
            {
                throw new ArgumentNullException();
            }
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
