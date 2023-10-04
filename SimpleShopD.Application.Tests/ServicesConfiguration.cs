using Microsoft.Extensions.DependencyInjection;
using SimpleShopD.Domain.Policies;

namespace SimpleShopD.Application.Tests
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection ConfigurePolicies(this  IServiceCollection services)
        {
            services.AddScoped<ICreateUserPolicy, CreateUserPolicy>();
            services.AddScoped<ICreateUserPolicy, CreateAdminPolicy>();
            return services;
        }
    }
}
