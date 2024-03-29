﻿using System.Text.Json;
using WebMVC.Models.DTOs;
using WebMVC.Infrastructure.API;
using WebMVC.ViewModels.RoutingViewModels;
using WebMVC.Models;

namespace WebMVC.Services;

public class RoutingService : IRoutingService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<RoutingService> _logger;

    private readonly string _remoteServiceUrl;

    public RoutingService(HttpClient httpClient, ILogger<RoutingService> logger, IConfiguration configuration)
    {
        _httpClient=httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _logger=logger ?? throw new ArgumentNullException(nameof(logger));

        // Microservices -> Routing -> url -> smth_url.
        _remoteServiceUrl=configuration
            .GetSection("Microservices")
            .GetSection("Routing")
            .GetSection("url").Value ?? throw new ArgumentNullException(nameof(configuration));
    }

    public async Task<RoutingViewModel> GetDeliveryInfo(CityDTO cityDTO)
    {
        var uri = API.Routing.GetDeliveryInfo(_remoteServiceUrl, cityDTO);
        var response = await _httpClient.GetStringAsync(uri);

        var viewModel = JsonSerializer.Deserialize<RoutingViewModel>(response, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return viewModel;
    }

    public async Task<IEnumerable<CitiesName>> LoadCities()
    {
        var uri = API.Routing.LoadCities(_remoteServiceUrl);
        List<CitiesName>? citiesNames = new();
        try
        {
            var response = await _httpClient.GetStringAsync(uri);
            var citiesDto = JsonSerializer.Deserialize<IEnumerable<CityNameDTO>>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (citiesDto is not null)
            {
                foreach (var city in citiesDto)
                {
                    var temp = new CitiesName(city.Name);

                    citiesNames.Add(temp);
                }
            }
        }
        catch (HttpRequestException)
        {
            _logger.LogError(
                "Routing mircoservice return error. Service: {service}, Time: {time}.",
                nameof(RoutingService),
                DateTime.UtcNow.ToLongDateString());
        }
        catch (JsonException)
        {
            _logger.LogCritical(
                "Cannot read json-response. Service: {service}, Time: {time}.",
                nameof(RoutingService),
                DateTime.UtcNow.ToLongDateString());
        }
        catch (ArgumentNullException)
        {
            _logger.LogCritical(
                "Response have nullable value. Service: {service}, Time: {time}.",
                nameof(RoutingService),
                DateTime.UtcNow.ToLongDateString());
        }

        return citiesNames;
    }

    public async Task<bool> ConfirmCargoCreating(UserPaymentDTO userPaymentDTO)
    {
        var uri = API.Routing.CreateCargo(_remoteServiceUrl);
        var paymentContext = new StringContent(JsonSerializer.Serialize(userPaymentDTO), System.Text.Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(uri, paymentContext);

        if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
        {
            _logger.LogCritical(
                "Cannot create cargo. Service: {service}, Time: {time}.",
                nameof(RoutingService),
                DateTime.UtcNow.ToLongDateString());

            return false;
        }

        return true;
    }
}
