using Microsoft.Extensions.DependencyInjection;

namespace SimpleShopD.Domain
{
    public static class Extensions
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
            => services;
    }
}
