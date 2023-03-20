using Transportation.API.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "Redis";
});
builder.Services.AddMongo(builder.Configuration.GetSection("Mongo"));
builder.Services.AddCalculationPrice();
builder.Services.AddTransportRepository();

var app = builder.Build();

app.MapControllers();

app.Run();
