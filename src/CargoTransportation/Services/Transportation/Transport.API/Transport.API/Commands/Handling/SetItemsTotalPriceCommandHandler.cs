using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using Transportation.API.Commands.Command;
using Transportation.API.Services;

namespace Transportation.API.Commands.Handling;

public class SetItemsTotalPriceCommandHandler : IRequestHandler<SetItemsTotalPriceCommand, bool>
{
    private readonly IDistributedCache _cache;
    private readonly ILogger<SetItemsTotalPriceCommandHandler> _logger;

    public SetItemsTotalPriceCommandHandler(IDistributedCache cache, ILogger<SetItemsTotalPriceCommandHandler> logger)
    {
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<bool> Handle(SetItemsTotalPriceCommand request, CancellationToken cancellationToken)
    {
        // Calculated price all items without delivery part.
        var keyTotalPrice = KeyMaker.CreateKey(request.UserId, "totalPrice");
        // Need for calculating total price with delivery part. 
        var keyTransportOptions = KeyMaker.CreateKey(request.UserId, "options");
        try
        {
            var jsonTotalPrice = JsonSerializer.Serialize(request.TotalPrice);
            var jsonTransportOptions = JsonSerializer.Serialize(request.Option);

            await _cache.SetStringAsync(keyTotalPrice, jsonTotalPrice, cancellationToken);
            await _cache.SetStringAsync(keyTransportOptions, jsonTransportOptions, cancellationToken);

            return true;
        }
        catch (ArgumentNullException)
        {
            _logger.LogCritical(
                "Memory cache is not available. Handler: {handler}",
                nameof(SetItemsTotalPriceCommandHandler));

            return false;
        }
        catch (NotSupportedException)
        {
            _logger.LogCritical(
                "Not supported value in json. Handler: {handler}",
                nameof(SetItemsTotalPriceCommandHandler));

            return false;
        }
    }
}
