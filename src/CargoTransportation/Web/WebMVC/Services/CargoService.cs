using System.Net;
using System.Text.Json;
using WebMVC.Models.DTOs;
using WebMVC.Infrastructure.API;
using WebMVC.ViewModels.CargoViewModels;

namespace WebMVC.Services;

public class CargoService : ICargoService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<CargoService> _logger;

    private readonly string _remoteServiceUrl;

    public CargoService(HttpClient httpClient, ILogger<CargoService> logger, IConfiguration configuration)
    {
        _httpClient=httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _logger=logger ?? throw new ArgumentNullException(nameof(logger));

        // Microservices -> Cargo -> url -> smth_url.
        _remoteServiceUrl=configuration
            .GetSection("Microservices")
            .GetSection("Cargo")
            .GetSection("url").Value ?? throw new ArgumentNullException(nameof(configuration));
    }

    public async Task AddItemsAsync(UserItemsDTO userItems)
    {
        var uri = API.Cargo.AddItems(_remoteServiceUrl);
        var userItemsContent = new StringContent(JsonSerializer.Serialize(userItems), System.Text.Encoding.UTF8, "application/json");

        try
        {
            var response = await _httpClient.PostAsync(uri, userItemsContent);

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new InvalidOperationException();
            }

            response.EnsureSuccessStatusCode();
        }
        catch (InvalidOperationException)
        {
            _logger.LogCritical(
                    "Cannot add items. Service:{service} Time: {Time}.",
                    nameof(CargoService),
                    DateTime.UtcNow.ToLongDateString());
        }
        catch (HttpRequestException)
        {
            _logger.LogError(
                    "Microservice Cargo is dead. Service:{service} Time: {Time}.",
                    nameof(CargoService),
                    DateTime.UtcNow.ToLongDateString());
        }
        catch (JsonException)
        {
            _logger.LogCritical(
                "Cannot read json-response. Service: {service}, Time: {time}.",
                nameof(CargoService),
                DateTime.UtcNow.ToLongDateString());
        }
    }

    public async Task<IEnumerable<CargoViewModel>?> GetCargoInfoAsync(string userId)
    {
        List<CargoViewModel>? cargoInfoViewModel = null;
        var uri = API.Cargo.GetCargo(_remoteServiceUrl, userId);

        try
        {
            var response = await _httpClient.GetStringAsync(uri);

            var cargoInfoDto = JsonSerializer.Deserialize<IEnumerable<CargoInfoDTO>>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (cargoInfoDto is not null)
            {
                cargoInfoViewModel = new();
                foreach (var cargoInfo in cargoInfoDto)
                {
                    CargoViewModel cargoViewModel = new
                    (
                        City: cargoInfo.City,
                        Items: cargoInfo.Items,
                        Price: cargoInfo.Price,
                        Time: cargoInfo.Time
                    );

                    cargoInfoViewModel.Add(cargoViewModel);
                }
            }
        }
        catch (HttpRequestException)
        {
            _logger.LogError(
                "Cargo mircoservice return error. Service: {service}, Time: {time}.",
                nameof(CargoService),
                DateTime.UtcNow.ToLongDateString());
        }
        catch (JsonException)
        {
            _logger.LogCritical(
                "Cannot read json-response. Service: {service}, Time: {time}.",
                nameof(CargoService),
                DateTime.UtcNow.ToLongDateString());
        }

        return cargoInfoViewModel;
    }
}
