using System.Net;
using System.Text.Json;
using WebMVC.Infrastructure.API;
using WebMVC.Models;

namespace WebMVC.Services;

public class AccountService : IAccountService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<AccountService> _logger;

    private readonly string _remoteServiceUrl;

    public AccountService(HttpClient httpClient, ILogger<AccountService> logger, IConfiguration configuration)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        // Microservices -> Account -> url -> smth_url.
        _remoteServiceUrl = configuration
            .GetSection("Microservices")
            .GetSection("Account")
            .GetSection("url").Value ?? throw new ArgumentNullException(nameof(configuration));
    }

    public async Task<bool> AddAsync(User user)
    {
        var uri = API.Account.AddUser(_remoteServiceUrl);
        var userContent = new StringContent(JsonSerializer.Serialize(new {user.Login, user.Password}), System.Text.Encoding.UTF8, "application/json");

        bool isCreated = false;

        try
        {
            var response = await _httpClient.PostAsync(uri, userContent);
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                isCreated = false;
                
                throw new InvalidOperationException();
            }

            isCreated = JsonSerializer.Deserialize<bool>(response.Content.ReadAsStream(), new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
        catch (InvalidOperationException)
        {
            _logger.LogCritical(
                    "Cannot add user. Service:{service} Time: {Time}.",
                    nameof(AccountService),
                    DateTime.UtcNow.ToLongDateString());
        }
        catch (HttpRequestException)
        {
            _logger.LogError(
                "Account mircoservice return error. Service: {service}, Time: {time}.",
                nameof(AccountService),
                DateTime.UtcNow.ToLongDateString());
        }
        catch (JsonException)
        {
            _logger.LogCritical(
                "Cannot read json-response. Service: {service}, Time: {time}.",
                nameof(AccountService),
                DateTime.UtcNow.ToLongDateString());
        }

        return isCreated;
    }

    public async Task<User?> GetAsync(User user)
    {
        var uri = API.Account.GetUser(_remoteServiceUrl, user.Login, user.Password);
        User? account = null;

        try
        {
            var response = await _httpClient.GetStringAsync(uri);

            account = JsonSerializer.Deserialize<User>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
        catch (HttpRequestException)
        {
            _logger.LogError(
                "Account mircoservice return error. Service: {service}, Time: {time}.",
                nameof(AccountService),
                DateTime.UtcNow.ToLongDateString());
        }
        catch (JsonException)
        {
            _logger.LogCritical(
                "Cannot read json-response. Service: {service}, Time: {time}.",
                nameof(AccountService),
                DateTime.UtcNow.ToLongDateString());
        }

        return account;
    }
}
