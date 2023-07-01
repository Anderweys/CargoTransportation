using System.Collections.Generic;
using System.Text.Json;
using WebMVC.Infrastructure.API;
using WebMVC.Models;
using WebMVC.Models.DTOs;

namespace WebMVC.Services;

public class TransportService : ITransportService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<TransportService> _logger;

    private readonly string _remoteServiceUrl;

    public TransportService(HttpClient httpClient, ILogger<TransportService> logger, IConfiguration configuration)
    {
        _httpClient=httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _logger=logger ?? throw new ArgumentNullException(nameof(logger));

        // Microservices -> Transport -> url -> smth_url.
        _remoteServiceUrl=configuration
            .GetSection("Microservices")
            .GetSection("Transport")
            .GetSection("url").Value ?? throw new ArgumentNullException(nameof(configuration));
    }

    public async Task<IEnumerable<Item>> GetUserItems(string userId)
    {
        var uri = API.Transport.GetUserItems(_remoteServiceUrl, userId);
        IEnumerable<Item>? items = null;
        try
        {
            var response = await _httpClient.GetStringAsync(uri);

            items = JsonSerializer.Deserialize<IEnumerable<Item>>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
        catch (HttpRequestException)
        {
            _logger.LogError(
                "Routing mircoservice return error. Service: {service}, Time: {time}.",
                nameof(TransportService),
                DateTime.UtcNow.ToLongDateString());
        }
        catch (JsonException)
        {
            _logger.LogCritical(
                "Cannot read json-response. Service: {service}, Time: {time}.",
                nameof(TransportService),
                DateTime.UtcNow.ToLongDateString());
        }

        return items;
    }

    public async Task<TransportInfo> GetTransportInfo(TransportTypeDTO transportTypeDTO)
    {
        var uri = API.Transport.GetTransportInfo(_remoteServiceUrl, transportTypeDTO);

        TransportInfo? transportInfo = null;
        try
        {
            var response = await _httpClient.GetStringAsync(uri);

            transportInfo = JsonSerializer.Deserialize<TransportInfo>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
        catch (HttpRequestException)
        {
            _logger.LogError(
                "Routing mircoservice return error. Service: {service}, Time: {time}.",
                nameof(TransportService),
                DateTime.UtcNow.ToLongDateString());
        }
        catch (JsonException)
        {
            _logger.LogCritical(
                "Cannot read json-response. Service: {service}, Time: {time}.",
                nameof(TransportService),
                DateTime.UtcNow.ToLongDateString());
        }

        return transportInfo;
    }

    public async Task<IEnumerable<TransportType>> GetTransporType()
    {
        var uri = API.Transport.GetTransportType(_remoteServiceUrl);
        IEnumerable<TransportType>? transportInfo = null;
        try
        {
            var response = await _httpClient.GetStringAsync(uri);

            transportInfo = JsonSerializer.Deserialize<IEnumerable<TransportType>>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
        catch (HttpRequestException)
        {
            _logger.LogError(
                "Routing mircoservice return error. Service: {service}, Time: {time}.",
                nameof(TransportService),
                DateTime.UtcNow.ToLongDateString());
        }
        catch (JsonException)
        {
            _logger.LogCritical(
                "Cannot read json-response. Service: {service}, Time: {time}.",
                nameof(TransportService),
                DateTime.UtcNow.ToLongDateString());
        }

        return transportInfo;
    }
}