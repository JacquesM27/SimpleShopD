using Microsoft.Extensions.DependencyInjection;
using SimpleShopD.Domain.Policies;

namespace SimpleShopD.Domain
{
    public static class Extensions
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            services.Scan(x => x.FromAssemblies(typeof(ICreateUserPolicy).Assembly)
                .AddClasses(y => y.AssignableTo<ICreateUserPolicy>())
                .AsImplementedInterfaces()
                .WithSingletonLifetime());

            return services;
        }
    }
}
