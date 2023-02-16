using CargoObject.Domain.AggregatesModel.CargoAggregates;
using CargoObject.Domain.AggregatesModel.LoaderAggragates;
using CargoObject.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CargoObject.Application.DomainEventHandlers;


public class LoadFinishedDomainEventHandler
    : INotificationHandler<LoadFinishedDomainEvent>
{
    private readonly ILoaderRepository _loaderRepository;
    private readonly ICargoRepository _cargoRepository;
    private readonly ILoggerFactory _logger;

    public LoadFinishedDomainEventHandler(
        ILoaderRepository loaderRepository, 
        ICargoRepository cargoRepository, 
        ILoggerFactory logger)
    {
        _loaderRepository=loaderRepository;
        _cargoRepository=cargoRepository;
        _logger=logger;
    }

    public async Task Handle(LoadFinishedDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.CreateLogger<LoadFinishedDomainEvent>()
            .LogTrace($"Load has finished by loaderId: {notification.LoaderId}.");

        var cargos = await _cargoRepository.GetAllAsyncByLoaderId(notification.LoaderId);
        var loader = await _loaderRepository.GetById(notification.LoaderId);

        // TODO: CargosLoadedIntegrationEvents.
    }
}
