using Account.API.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(s =>
{
    s.SwaggerDoc(
        "v1",
        new()
        {
            Title = "Account.API",
            Version = "v1"
        }
    );
});
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});
builder.Services.AddAccountContext(builder.Configuration);
builder.Services.AddAccountRepository();

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