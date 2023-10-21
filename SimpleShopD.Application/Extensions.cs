using Microsoft.Extensions.DependencyInjection;

namespace SimpleShopD.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
            => services;
    }
}
