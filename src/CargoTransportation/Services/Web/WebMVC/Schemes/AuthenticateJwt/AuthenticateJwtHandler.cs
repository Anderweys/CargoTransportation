using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Encodings.Web;
using WebMVC.Infrastructure;

namespace WebMVC.Schemes.AuthenticateJwt;

/// <summary>
/// When use redirect on "authorize" action, 
/// before we need to write down in header Jwt-token,
/// but "window.location.redirect" don't contain header,
/// and it returns 401/403 code if redirect to action with "Authorize" attribute.
/// So we will use our authorize attribute
/// that to validate Jwt-token from query, not header.
/// </summary>
public class AuthenticateJwtHandler : AuthenticationHandler<AuthenticateJwtOptions>
{
    public AuthenticateJwtHandler(
        IOptionsMonitor<AuthenticateJwtOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock)
        : base(options, logger, encoder, clock)
    {
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var token = Context.Request.Query["token"];

        if (!string.IsNullOrEmpty(token))
        {
            var isValidToken = VerifyToken(token);

            if (isValidToken)
            {
                var identity = new ClaimsIdentity(new List<Claim>() { new(ClaimTypes.Name, token) }, Scheme.Name);
                var principal = new System.Security.Principal.GenericPrincipal(identity, null);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);

                return Task.FromResult(AuthenticateResult.Success(ticket));
            }
        }
        return Task.FromResult(AuthenticateResult.Fail("No access jwt-token."));
    }

    private bool VerifyToken(string token)
    {
        JwtSecurityTokenHandler tokenHandler = new();
        SecurityToken? validatedToken;

        try
        {
            tokenHandler.ValidateToken(
            token,
            Authentication.GetValidationParameters(),
            out validatedToken);
        }
        catch (SecurityTokenException)
        {
            Logger.LogWarning("Validating Jwt-token exception. " +
                "Someone change token signature.\n\tTime: {time}\n\tChanged token: {token}",
                Clock.UtcNow, token);
            return false;
        }
        catch (Exception ex)
        {
            Logger.LogCritical("Critical exception by validate token.\n\tException: {exception}" +
                "\n\tTime: {time}\n\tChanged token: {token}",
                ex.Message, Clock.UtcNow, token);
            throw;
        }

        return validatedToken is not null;
    }
}
