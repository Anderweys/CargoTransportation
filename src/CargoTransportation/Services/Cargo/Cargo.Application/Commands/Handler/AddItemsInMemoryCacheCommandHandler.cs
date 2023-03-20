using MediatR;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Distributed;
using CargoObject.Application.Commands.Command;

namespace CargoObject.Application.Commands.Handler;

public class AddItemsInMemoryCacheCommandHandler : IRequestHandler<AddItemsInMemoryCacheCommand, bool>
{
    private readonly IDistributedCache _cache;
    private readonly ILogger<AddItemsInMemoryCacheCommandHandler> _logger;

    public AddItemsInMemoryCacheCommandHandler(IDistributedCache cache, ILogger<AddItemsInMemoryCacheCommandHandler> logger)
    {
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<bool> Handle(AddItemsInMemoryCacheCommand request, CancellationToken cancellationToken)
    {
        var data = JsonSerializer.Serialize(request.Items);

        try
        {
            await _cache.SetStringAsync(request.UserId, data, cancellationToken);
        }
        catch (ArgumentNullException)
        {
            _logger.LogError(
                "Memory cache is not available. Handler: {handler}, time: {time}.",
                nameof(AddItemsInMemoryCacheCommandHandler),
                DateTime.UtcNow.ToLongDateString());

            return false;
        }

        return true;
    }
}
