using MediatR;
using MassTransit;
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using Routing.API.Models;
using Contracts.IntegrationEvents;

namespace Routing.API.Commands;

public class CreateCargoCommandHandler : IRequestHandler<CreateCargoCommand, bool>
{
    private readonly IPublishEndpoint _endpoint;
    private readonly IDistributedCache _cache;
    private readonly ILogger<CreateCargoCommandHandler> _logger;

    public CreateCargoCommandHandler(ILogger<CreateCargoCommandHandler> logger, IPublishEndpoint endpoint, IDistributedCache cache)
    {
        _logger=logger ?? throw new ArgumentNullException(nameof(logger));
        _endpoint=endpoint ?? throw new ArgumentNullException(nameof(endpoint));
        _cache=cache ?? throw new ArgumentNullException(nameof(cache));
    }

    public async Task<bool> Handle(CreateCargoCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var itemsString = await _cache.GetStringAsync(request.UserId, cancellationToken);
            var items = JsonSerializer.Deserialize<IEnumerable<Item>>(itemsString);

            var integrationEvent = new CreateCargoIntegrationEvent(
                request.UserId,
                request.City,
                request.Time,
                request.Money,
                items);

            await _endpoint.Publish(integrationEvent, cancellationToken);

            return true;
        }
        catch (ArgumentNullException e)
        {
            _logger.LogCritical(
                "Delivery info was not created." +
                "No cache items data. Maybe problems:\n" +
                "\t- Out data lifetime.\n" +
                "\t- Code not added data in cache.\n" +
                "\t- Distributed cache service is dead.\n" +
                " \n\tCommand handler: {handler},\n\tException: {e}, message: {m}, stack trace: {st},\n\t Time: {time}.",
                nameof(ArgumentNullException),
                e.Message,
                e.StackTrace,
                nameof(CreateCargoCommandHandler),
                DateTime.UtcNow.ToLongDateString());
        }
        catch (Exception ex)
        {
            _logger.LogError(
                "Error when appearing to the external services (Redis, Mongo etc.)." +
                "\n\tCommand handler: {handler},\n\tException: {mes}, stack trace: {st},\n\tTime: {time}.",
                nameof(GetDeliveryInfoCommandHandler),
                ex.Message,
                ex.StackTrace,
                DateTime.UtcNow.ToLongDateString());
        }

        return false;
    }
}
