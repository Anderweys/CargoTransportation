using Cargo.Domain.AggregatesModel.CargoAggregates;
using Cargo.Domain.AggregatesModel.LoaderAggragates;
using Cargo.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Cargo.Application.DomainEventHandlers;

/// <summary>
/// It's when to add a cargo using a loader.
/// </summary>
public class CargoPlacedDomainEventHandler 
    : INotificationHandler<CargoPlacedDomainEvent>
{
    private readonly ICargoRepository _cargoRepository;
    private readonly ILoaderRepository _loaderRepository;
    private readonly ILoggerFactory _logger;

    public CargoPlacedDomainEventHandler(ICargoRepository cargoRepository, ILoaderRepository loaderRepository, ILoggerFactory logger)
    {
        _cargoRepository = cargoRepository;
        _loaderRepository = loaderRepository;
        _logger = logger;
    }

    public async Task Handle(CargoPlacedDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.CreateLogger<CargoPlacedDomainEvent>()
            .LogTrace($"Cargo with id: {notification.Cargo.Id} is beginning to load\n" +
                      $"by loader with id: {notification.LoaderId}.");

        var cargos = await _cargoRepository.GetAllAsyncByLoaderId(notification.LoaderId);
        var loader = await _loaderRepository.GetById(notification.LoaderId);

        loader.LoadCargos(cargos);

        _loaderRepository.Add(loader);

        await _loaderRepository.UnitOfWork.SaveChangesAsync();
    }
}
