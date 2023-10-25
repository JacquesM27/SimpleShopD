using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SimpleShopD.Shared.Middlewares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleShopD.Shared
{
    public static class Extensions
    {
        public static IServiceCollection AddShared(this IServiceCollection services)
        {
            services.AddScoped<CustomExceptionMiddleware>();
            return services;
        }

        public static IApplicationBuilder UseShared(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomExceptionMiddleware>();
            return app;
        }
    }
}
