using WebMVC.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddHttpClientServices();

var app = builder.Build();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Cargo}/{action}",
    defaults: new {controller = "Cargo", action = "Index"});

app.Run();
