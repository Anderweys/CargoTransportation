var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Cargo}/{action}",
    defaults: new {controller = "Cargo", action = "Index"});

app.Run();
