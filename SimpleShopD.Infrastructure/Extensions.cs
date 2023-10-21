using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleShopD.Domain.Repositories;
using SimpleShopD.Domain.Users;
using SimpleShopD.Infrastructure.EF.Contexts;
using SimpleShopD.Infrastructure.EF.Repositories;

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
            return services;
        }

        private static IServiceCollection AddMssql(this IServiceCollection services, IConfiguration configuration)
        {
            var section = configuration.GetSection("mssql");
            var options = new MssqlOptions();
            section.Bind(options);
            services.Configure<MssqlOptions>(section);

            services.AddDbContext<WriteDbContext>(x => x.UseSqlServer(options.ConnectionString));

            using (var scope =  services.BuildServiceProvider().CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<WriteDbContext>();
                dbContext!.Database.Migrate();
                dbContext!.AddDefaultAdminAccount();
            }

            return services;
        }

        private static void AddDefaultAdminAccount(this WriteDbContext dbContext)
        {
            if (dbContext.Users.Any())
                return;

            var defaultAdmin = new User(Guid.NewGuid(), new("Jacob", "Admin"), "admin@simpleshop.com", new("12345Abc!"), new(Domain.Enum.UserRole.Admin));
            defaultAdmin.Activate(defaultAdmin.ActivationToken!.Value);

            dbContext.Users.Add(defaultAdmin);
            dbContext.SaveChanges();
        }
    }
}
