using Account.API.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});
builder.Services.AddAccountContext(builder.Configuration);
builder.Services.AddAccountRepository();

var app = builder.Build();

app.MapControllers();

app.Run();