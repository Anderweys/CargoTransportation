using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebMVC.Infrastructure;

public class Authorization
{
    public const string ISSUER = "WebMVC"; // издатель токена
    public const string AUDIENCE = "MyAuthClient"; // потребитель токена
    const string KEY = "blyad tut doljno byt bolshe 16 symbolv, inache ne rabotaet, suka takaya.";   // ключ для шифрации
    public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
        new(Encoding.UTF8.GetBytes(KEY));


    //    var pidarasy = new List<Gandony>
    //{
    //    new("HuiSGory", "1234"),
    //    new("Pidrila", "qwerty")
    //};

    //    string token = string.Empty;

    //    builder.Services.AddAuthorization();
    //builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    //    .AddJwtBearer(cfg =>
    //    {
    //        cfg.TokenValidationParameters = new()
    //        {
    //            ValidateIssuer = true,
    //            ValidIssuer = AuthOptions.ISSUER,
    //            ValidateAudience = true,
    //            ValidAudience= AuthOptions.AUDIENCE,
    //            ValidateLifetime = true,
    //            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
    //            ValidateIssuerSigningKey = true
    //        };
    //    });


    //var app = builder.Build();

    //    app.Map("/login", (Gandony eblan) =>
    //{
    //    var huesos = pidarasy
    //    .Find(p => p.Email == eblan.Email && p.Password == eblan.Password);

    //    if(huesos is null)
    //    {
    //        // Вот тут должен быть редирект на авторизацию мерзавца.
    //        // Или кидать гаду 401 ошибку.

    //        //return Results.Unauthorized();
    //    }

    //    var claims = new List<Claim> { new Claim(ClaimTypes.Name, huesos.Email) };
    //    var jwt = new JwtSecurityToken(
    //            issuer: AuthOptions.ISSUER,
    //            audience: AuthOptions.AUDIENCE,
    //            claims: claims,
    //            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)), // время действия 2 минуты
    //            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

    //    var eJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

    //    var response = new
    //    {
    //        access_token = eJwt,
    //        username = huesos.Email
    //    };

    //    token = eJwt;

    //    return Results.Json(response);
    //});

    //app.Map("/data", [Authorize] () => "Pidaras!");

    //app.Run();

    //public class AuthOptions
    //{
    //    public const string ISSUER = "MyAuthServer"; // издатель токена
    //    public const string AUDIENCE = "MyAuthClient"; // потребитель токена
    //    const string KEY = "blyad tut doljno byt bolshe 16 symbolv, inache ne rabotaet, suka takaya.";   // ключ для шифрации
    //    public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
    //        new(Encoding.UTF8.GetBytes(KEY));
    //}

    //class Gandony
    //{
    //    public string Email { get; set; }
    //    public string Password { get; set; }

    //    public Gandony(string email, string password)
    //    {
    //        Email=email??throw new ArgumentNullException(nameof(email));
    //        Password=password??throw new ArgumentNullException(nameof(password));
    //    }
    //}
}
