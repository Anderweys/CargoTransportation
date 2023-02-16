using CargoObject.Domain.AggregatesModel.CargoAggregates;
using CargoObject.Domain.AggregatesModel.LoaderAggragates;
using CargoObject.Domain.Events;
using Microsoft.Extensions.Logging;
using MediatR;
using CargoObject.Application.IntegrationEvent.Event;
using MassTransit;

namespace CargoObject.Application.DomainEventHandlers;

public class LoadFinishedDomainEventHandler
    : INotificationHandler<LoadFinishedDomainEvent>
{
    private readonly ILoaderRepository _loaderRepository;
    private readonly ICargoRepository _cargoRepository;
    private readonly ILoggerFactory _logger;
    private readonly IPublishEndpoint _publishEndpoint;

    public LoadFinishedDomainEventHandler(
        ILoaderRepository loaderRepository,
        ICargoRepository cargoRepository,
        ILoggerFactory logger,
        IPublishEndpoint publisherEndpoint)
    {
        _loaderRepository=loaderRepository;
        _cargoRepository=cargoRepository;
        _logger=logger;
        _publishEndpoint=publisherEndpoint;
    }

    public async Task Handle(LoadFinishedDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.CreateLogger<LoadFinishedDomainEvent>()
            .LogTrace($"Load has finished by loaderId: {notification.LoaderId}.");

        var cargos = await _cargoRepository.GetAllAsyncByLoaderId(notification.LoaderId);
        var loader = await _loaderRepository.GetAsyncById(notification.LoaderId);

        var cargosLoadedIE = new CargosLoadedIntegrationEvent(loader, cargos.ToList());
        await _publishEndpoint.Publish(cargosLoadedIE);
    }
}
