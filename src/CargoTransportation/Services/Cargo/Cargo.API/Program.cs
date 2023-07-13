using System.Reflection;
using CargoObject.API.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSwaggerGen(s =>
{
    s.SwaggerDoc(
        "v1",
        new()
        {
            Title = "Cargo.API",
            Version = "v1"
        }
    );
});
builder.Services.AddControllers();
builder.Services.AddCargoRepository();
builder.Services.AddCargoContext(builder.Configuration);
builder.Services.AddMediatR(config =>
{
    /* Unfortunately this don't work:
    *
    * config.RegisterServicesFromAssembly(typeof(Program).Assembly);
    * 
    * Because we use Commands and Handlers from Library (Cargo.Application), another assembly.
    * And this assembly (Cargo.API) doesn't see our Commands and Handlers from our Library (Cargo.Application).
    * So we need to add all library (as assembly) that contains MediatR.
    */
    
    Assembly.GetAssembly(typeof(Program)).GetReferencedAssemblies()                     // Get all assemblies name this solutions.
        .Where(a => a.Name.Contains("Cargo"))                                           // Search our assemblies with MediatR.
        .ToList()                                                                       
        .ForEach(a =>
        {
            config.RegisterServicesFromAssembly(AppDomain.CurrentDomain.Load(a.Name));  // Registration our assembly in MediatR.
        });

    /* Analog:
    *
    *   var cargoAppAssembly = AppDomain.CurrentDomain.Load("Cargo.Application);
    *   var cargoInfraAssembly = AppDomain.CurrentDomain.Load("Cargo.Infrastructure);
    *   var cargoApiAssembly = typeof(Program).Assembly;
    *   config.RegisterServicesFromAssemlies(cargoAppAssembly, cargoInfraAssembly, cargoApiAssembly);
    */
});
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "Redis";
});
builder.Services.AddMassTransitRabbitMQ(builder.Configuration.GetSection("RabbitMQ"));

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