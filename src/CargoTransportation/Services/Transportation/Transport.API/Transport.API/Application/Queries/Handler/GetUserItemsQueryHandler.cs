using MediatR;
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using Transportation.API.Application.Models.DTOs;
using Transportation.API.Application.Queries.Query;

namespace Transportation.API.Application.Queries.Handler;

public class GetUserItemsQueryHandler : IRequestHandler<GetUserItemsQuery, IEnumerable<ItemDTO>>
{
    private readonly IDistributedCache _cache;
    private readonly ILogger<GetUserItemsQueryHandler> _logger;

    public GetUserItemsQueryHandler(IDistributedCache cache, ILogger<GetUserItemsQueryHandler> logger)
    {
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<IEnumerable<ItemDTO>> Handle(GetUserItemsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var userItemsString = await _cache.GetStringAsync(request.UserId) ?? throw new ArgumentNullException(nameof(request));
            var items = JsonSerializer.Deserialize<IEnumerable<ItemDTO>>(userItemsString);

            return items;
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
                nameof(GetUserItemsQueryHandler),
                DateTime.UtcNow.ToLongDateString());

            return new List<ItemDTO>();
        }
        catch (Exception ex)
        {
            _logger.LogError(
                "Error when appearing to the external services (Redis, Mongo etc.)." +
                "\n\tCommand handler: {handler},\n\tException: {mes}, stack trace: {st},\n\tTime: {time}.",
                nameof(GetUserItemsQueryHandler),
                ex.Message,
                ex.StackTrace,
                DateTime.UtcNow.ToLongDateString());

            return new List<ItemDTO>();
        }
    }
}
