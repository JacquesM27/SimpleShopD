using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleShopD.Domain.Repositories;
using SimpleShopD.Domain.Services;
using SimpleShopD.Domain.Users;
using SimpleShopD.Domain.Users.ValueObjects;
using SimpleShopD.Infrastructure.EF.Contexts;
using SimpleShopD.Infrastructure.EF.Repositories;
using SimpleShopD.Infrastructure.HostedServices;
using SimpleShopD.Infrastructure.Logging;
using SimpleShopD.Infrastructure.Services;
using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMssql(configuration);
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddTransient<ITokenProvider, TokenProvider>();
            services.AddTransient<IContextAccessor, ContextAccessor>();

            services.TryDecorate(typeof(ICommandHandler<>), typeof(CommandHandlerLoggerDecorator<>));
            services.TryDecorate(typeof(ICommandTResultHandler<,>), typeof(CommandTValueHandlerLoggerDecorator<,>));

            return services;
        }

        private static IServiceCollection AddMssql(this IServiceCollection services, IConfiguration configuration)
        {
            var section = configuration.GetSection("mssql");
            var options = new MssqlOptions();
            section.Bind(options);
            services.Configure<MssqlOptions>(section);

            services.AddDbContext<WriteDbContext>(x => x.UseSqlServer(options.ConnectionString));
            services.AddHostedService<MigrationHostedService>();

            return services;
        }

        internal static void AddDefaultAdminAccount(this WriteDbContext dbContext)
        {
            if (dbContext.Users.Any())
                return;

            var defaultAdmin = new User(Guid.NewGuid(), new("Jacob", "Admin"), "admin@simpleshop.com", new("12345Abc!"), Role.Admin);
            defaultAdmin.Activate(defaultAdmin.ActivationToken!.Value);

            dbContext.Users.Add(defaultAdmin);
            dbContext.SaveChanges();
        }
    }
}
