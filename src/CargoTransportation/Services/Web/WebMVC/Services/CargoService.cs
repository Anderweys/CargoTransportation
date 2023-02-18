using System.Text.Json;
using WebMVC.ViewModels;

namespace WebMVC.Services;

public class CargoService : ICargoService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<CargoService> _logger;

    public CargoService(HttpClient httpClient, ILogger<CargoService> logger)
    {
        _httpClient=httpClient;
        _logger=logger;
    }

    public async Task<Cargo> GetCargo()
    {
        var uri = "http://localhost:5250/api/v1/cargo";
        var response = await _httpClient.GetStringAsync(uri);

        var cargo = JsonSerializer.Deserialize<Cargo>(response, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        _logger.LogInformation($"CargoService here: {cargo.Name} {cargo.Email}");

        return cargo;
    }
}
