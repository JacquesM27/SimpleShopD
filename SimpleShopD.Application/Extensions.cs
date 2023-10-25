using Microsoft.Extensions.DependencyInjection;
using SimpleShopD.Shared.Commands;
using SimpleShopD.Shared.Queries;

namespace SimpleShopD.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddCommands();
            services.AddQueries();

            return services;
        }
    }
}
