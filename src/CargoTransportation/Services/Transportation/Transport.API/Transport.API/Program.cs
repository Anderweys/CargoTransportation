using Transportation.API.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(s =>
{
    s.SwaggerDoc(
        "v1",
        new()
        {
            Title = "Transportation.API",
            Version = "v1"
        }
    );
});
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

if (app.Environment.IsDevelopment())
{
    app
        .UseSwagger()
        .UseSwaggerUI(s =>
        {
            s.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
        });
    app.UseDeveloperExceptionPage();
}

app.MapControllers();

app.Run();
