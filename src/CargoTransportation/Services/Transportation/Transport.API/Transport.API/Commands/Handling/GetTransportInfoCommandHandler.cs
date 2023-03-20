using MediatR;
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using Transportation.API.Models;
using Transportation.API.Services;
using Transportation.API.Models.DTOs;
using Transportation.API.Commands.Command;

namespace Transportation.API.Commands.Handling;

public class GetTransportInfoCommandHandler : IRequestHandler<GetTransportInfoCommand, TransportInfoDTO>
{
    private readonly IMediator _mediator;
    private readonly IDistributedCache _cache;
    private readonly ITransportRepository _transportRepository;
    private readonly ILogger<GetTransportInfoCommandHandler> _logger;
    private readonly CalculationPriceService _calculationPrice;

    public GetTransportInfoCommandHandler(
        IMediator mediator,
        IDistributedCache cache,
        ITransportRepository transportRepository,
        ILogger<GetTransportInfoCommandHandler> logger,
        CalculationPriceService calculationPrice)
    {
        _mediator = mediator;
        _cache = cache;
        _transportRepository = transportRepository;
        _logger = logger;
        _calculationPrice = calculationPrice;
    }

    public async Task<TransportInfoDTO> Handle(GetTransportInfoCommand request, CancellationToken cancellationToken)
    {
        var userItemsString = await _cache.GetStringAsync(request.UserId);

        if (userItemsString is null)
        {
            _logger.LogCritical(
                "No cache items data. Command Handler: {handler} Maybe problems:\n" +
                "\t- Out data lifetime.\n" +
                "\t- Code not added data in cache.\n" +
                "\t- Distributed cache service is dead.\n",
                nameof(GetTransportInfoCommandHandler));

            throw new ArgumentNullException("No data in cache.");
        }

        var items = JsonSerializer.Deserialize<IEnumerable<ItemDTO>>(userItemsString);
        var price = _calculationPrice.CalculatePrice(request.TransportType, items);

        var transport = await _transportRepository.GetAsync(request.TransportName, request.TransportType);

        // Save total price for Routing Service. It need for calculate total price.
        var command = new SetItemsTotalPriceCommand(price, request.UserId, transport.Type, transport.AverageSpeed);
        await _mediator.Send(command);

        return new TransportInfoDTO(
            request.TransportType,
            items.Count(),
            price);
    }
}
