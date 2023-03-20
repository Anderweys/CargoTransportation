using Ocelot.DependencyInjection;
using Ocelot.Middleware;

// In LaunchSettings was commented docker config.
// For docker up need uncomment.

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("ocelot.json", false, true);
builder.Services.AddOcelot(builder.Configuration);
var app = builder.Build();

await app.UseOcelot();

app.Run();
