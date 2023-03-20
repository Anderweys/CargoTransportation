using MediatR;
using MassTransit;
using Microsoft.Extensions.Logging;
using CargoObject.Application.Commands.Command;
using Contracts.IntegrationEvents;

namespace CargoObject.Application.IntegrationEvents.Handlers;

public class CreateCargoIntegrationEventHandler : IConsumer<CreateCargoIntegrationEvent>
{
    private readonly IMediator _mediator;
    private readonly ILogger<CreateCargoIntegrationEventHandler> _logger;

    public CreateCargoIntegrationEventHandler(IMediator mediator, ILogger<CreateCargoIntegrationEventHandler> logger)
    {
        _mediator=mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger=logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task Consume(ConsumeContext<CreateCargoIntegrationEvent> context)
    {
        try
        {
            var command = new CreateCargoCommand(
                context.Message.UserId,
                context.Message.City,
                context.Message.Time,
                context.Message.Money,
                context.Message.Items);

            var isCreated = await _mediator.Send(command);

            if (!isCreated)
            {
                throw new Exception();
            }

            _logger.LogInformation(
                "Cargo has been added. Integration event: {event}, time: {time}.",
                nameof(CreateCargoIntegrationEventHandler),
                DateTime.UtcNow.ToLongDateString());
        }
        catch (Exception ex)
        {
            _logger.LogError(
                "Cannot create cargo. \n\tIntegration event: {event}, \n\tException: {ex}, \n\tTime: {time}.",
                nameof(CreateCargoIntegrationEventHandler),
                $"Message: {ex.Message}. Stack trace: {ex.StackTrace}",
                DateTime.UtcNow.ToLongDateString());
        }
    }
}