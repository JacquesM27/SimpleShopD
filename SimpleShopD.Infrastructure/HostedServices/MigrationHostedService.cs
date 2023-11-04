using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleShopD.Infrastructure.EF.Contexts;

namespace SimpleShopD.Infrastructure.HostedServices
{
    internal sealed class MigrationHostedService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public MigrationHostedService(IServiceProvider serviceProvider) 
            => _serviceProvider = serviceProvider;

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<WriteDbContext>();
                dbContext!.Database.Migrate();
                dbContext!.AddDefaultAdminAccount();
            }
            return Task.CompletedTask;
        }
    }
}
