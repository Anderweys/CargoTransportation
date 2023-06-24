// Old version. Now this application use HTTP.
//#define USEHTTPS

using WebMVC.Extensions;
#if USEHTTPS
using WebMVC.Schemes.AuthenticateJwt;
#endif

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddHttpClientServices();
builder.Services.AddControllerWebServices();
#if USEHTTPS
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(option =>
    {
        option.TokenValidationParameters = Authentication.GetValidationParameters();
    })
    .AddScheme<AuthenticateJwtOptions, AuthenticateJwtHandler>("AuthenticateJwt", null);
#endif
var app = builder.Build();

#if USEHTTPS
app.UseAuthentication();
app.UseAuthorization();
#endif

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Index}",
    defaults: new { controller = "Account", action = "Index" });

app.Run();