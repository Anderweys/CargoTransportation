using WebMVC.Extensions;
using WebMVC.Infrastructure;
using WebMVC.Schemes.AuthenticateJwt;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddHttpClientServices();
builder.Services.AddControllerWebServices();
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(option =>
    {
        option.TokenValidationParameters = Authentication.GetValidationParameters();
    })
    .AddScheme<AuthenticateJwtOptions, AuthenticateJwtHandler>("AuthenticateJwt", null);
var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Index}",
    defaults: new { controller = "Account", action = "Index" });

app.Run();