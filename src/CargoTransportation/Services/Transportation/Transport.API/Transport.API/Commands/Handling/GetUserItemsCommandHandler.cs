using MediatR;
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using Transportation.API.Models.DTOs;
using Transportation.API.Commands.Command;

namespace Transportation.API.Commands.Handling;

public class GetUserItemsCommandHandler : IRequestHandler<GetUserItemsCommand, IEnumerable<ItemDTO>>
{
    private readonly IDistributedCache _cache;
    private readonly ILogger<GetUserItemsCommandHandler> _logger;

    public GetUserItemsCommandHandler(IDistributedCache cache, ILogger<GetUserItemsCommandHandler> logger)
    {
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<IEnumerable<ItemDTO>> Handle(GetUserItemsCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var userItemsString = await _cache.GetStringAsync(request.UserId);

            if (userItemsString is null)
            {
                throw new ArgumentNullException();
            }
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
                nameof(GetUserItemsCommandHandler),
                DateTime.UtcNow.ToLongDateString());

            return new List<ItemDTO>();
        }
        catch (Exception ex)
        {
            _logger.LogError(
                "Error when appearing to the external services (Redis, Mongo etc.)." +
                "\n\tCommand handler: {handler},\n\tException: {mes}, stack trace: {st},\n\tTime: {time}.",
                nameof(GetUserItemsCommandHandler),
                ex.Message,
                ex.StackTrace,
                DateTime.UtcNow.ToLongDateString());

            return new List<ItemDTO>();
        }
    }
}
