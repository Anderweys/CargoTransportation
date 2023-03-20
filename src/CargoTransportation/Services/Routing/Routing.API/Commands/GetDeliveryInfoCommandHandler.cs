using MediatR;
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using Routing.API.Models;
using Routing.API.Models.DTOs;
using Routing.API.Infrastructure.Services;

namespace Routing.API.Commands;

public class GetDeliveryInfoCommandHandler : IRequestHandler<GetDeliveryInfoCommand, DeliveryInfoDTO>
{
    private readonly IMediator _mediator;
    private readonly IDistributedCache _cache;
    private readonly ICityRepository _cityRepository;
    private readonly ILogger<GetDeliveryInfoCommandHandler> _logger;

    public GetDeliveryInfoCommandHandler(
        IMediator mediator,
        IDistributedCache cache,
        ILogger<GetDeliveryInfoCommandHandler> logger,
        ICityRepository cityRepository)
    {
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _cityRepository=cityRepository ?? throw new ArgumentNullException(nameof(cityRepository));
    }

    public async Task<DeliveryInfoDTO> Handle(GetDeliveryInfoCommand request, CancellationToken cancellationToken)
    {
        var keyTotalPrice = KeyMaker.CreateKey(request.UserId, "totalPrice");
        var keyTransportOptions = KeyMaker.CreateKey(request.UserId, "options");

        try
        {
            var totalItemsPrice = await _cache.GetStringAsync(keyTotalPrice, token: cancellationToken);
            var transportOptionString = await _cache.GetStringAsync(keyTransportOptions, token: cancellationToken);
            var userItemsString = await _cache.GetStringAsync(request.UserId, token: cancellationToken);

            var city = await _cityRepository.GetAsync(request.City);

            if (DeliveryHandlerService.IsAnyNull(totalItemsPrice, transportOptionString, userItemsString, city))
            {
                throw new ArgumentNullException();
            }

            var totalPrice = JsonSerializer.Deserialize<float>(totalItemsPrice);
            var transportOption = JsonSerializer.Deserialize<TransportOptionDTO>(transportOptionString);
            var userItems = JsonSerializer.Deserialize<IEnumerable<Item>>(userItemsString);
            var itemInfos = Item.ParseToInfo(userItems);

            var deliveryPrice = DeliveryHandlerService.CalculateTotalPrice(
                totalPrice, transportOption.Type, transportOption.Speed, city.Distance);

            var deliveryInfo = new DeliveryInfoDTO(
                city.Name,
                DateTime.UtcNow.AddHours(city.Distance / transportOption.Speed),
                itemInfos,
                totalPrice);

            return deliveryInfo;
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

        // Default value.
        return new("", DateTime.MinValue, new List<ItemInfo>(), 0);
    }
}

public static class DeliveryHandlerService
{
    public enum TransportType : int
    {
        GROUND = 1,
        WATER = 2,
        AIR = 5
    }

    public static bool IsAnyNull(params object[] values)
        => values.Any(v => v is null);

    public static float CalculateTotalPrice(
        float totalItemsPrice,
        string transportType,
        float transportSpeed,
        float cityDistance)
    {
        float result = 0;

        var coef = transportType.ToLower() switch
        {
            "ground" => TransportType.GROUND,
            "water" => TransportType.WATER,
            "air" => TransportType.AIR,
            _ => throw new NotImplementedException("No transport type. Error in transport service!")
        };
        result = totalItemsPrice + cityDistance / transportSpeed * (int)coef;

        return result;
    }
}
