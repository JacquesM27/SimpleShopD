using Serilog;
using SimpleShopD.Application;
using SimpleShopD.Domain;
using SimpleShopD.Infrastructure;
using SimpleShopD.Shared;
using SimpleShopD.WebApi;

string policyName = "SomePolicyName";
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
                .WriteTo.File("/app/log/log.txt", rollingInterval: RollingInterval.Day, shared: true)
                .CreateLogger();
builder.Logging.AddSerilog();

builder.Services
    .AddShared()
    .AddDomain()
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.ConfigureSwagger()
    .ConfigureCors(policyName)
    .ConfigureAuthentication();

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseShared();

app.UseCors(policyName);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
