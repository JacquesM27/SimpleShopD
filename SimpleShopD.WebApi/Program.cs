using Serilog;
using SimpleShopD.Application;
using SimpleShopD.Domain;
using SimpleShopD.Infrastructure;
using SimpleShopD.Shared;
using SimpleShopD.WebApi;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
                .WriteTo.File("C:\\testSerilog\\log.txt", rollingInterval: RollingInterval.Day, shared: true)
                .CreateLogger();
builder.Logging.AddSerilog();

builder.Services
    .AddShared()
    .AddDomain()
    .AddApplication()
    .AddInfrastructure(builder.Configuration);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.ConfigureSwagger();
builder.ConfigureAuthentication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseShared();

app.UseAuthorization();

app.MapControllers();

app.Run();
