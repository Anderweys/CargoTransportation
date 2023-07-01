using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace WebMVC.Infrastructure;

public static class Authentication
{
    public const string ISSUER = "WebMVC";
    public const string AUDIENCE = "WebMVC";
    private const string KEY = "5766E59619EF3B1382355A40C4028C1545197E8282FAADE352E5C91A49A5970D";

    public static SymmetricSecurityKey GetSymmetricSecurityKey()
        => new(Encoding.UTF8.GetBytes(KEY));

    public static TokenValidationParameters GetValidationParameters()
        => new()
        {
            ValidIssuer = ISSUER,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = AUDIENCE,
            ValidateLifetime = true,
            IssuerSigningKey = GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true
        };

    public static JwtSecurityToken CreateToken(string login)
         => new(
            ISSUER,
            AUDIENCE,
            new List<Claim>() { new(ClaimTypes.Name, login) },
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: new SigningCredentials(
                GetSymmetricSecurityKey(),
                SecurityAlgorithms.HmacSha256)
            );

    public static string ParseToken(string token)
        => new JwtSecurityTokenHandler()
            .ValidateToken(
                token,
                GetValidationParameters(),
                out var validatedToken)
            .Identity.Name;
}
