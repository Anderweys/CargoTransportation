using WebMVC.Extensions;
using WebMVC.Schemes.AuthenticateJwt;
using WebMVC.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddHttpClientServices();
builder.Services.AddRepositoryService();
builder.Services.AddMongo(builder.Configuration.GetSection("Mongo"));
builder.Services.AddControllerWebServices();
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(option =>
    {
        option.TokenValidationParameters = Authentication.GetValidationParameters();
    })
    .AddScheme<AuthenticateJwtOptions, AuthenticateJwtHandler>("AuthenticateJwt", null);
builder.WebHost.UseKestrel(opt =>
{
    opt.ListenAnyIP(7242, cfg => cfg.UseHttps());
});
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