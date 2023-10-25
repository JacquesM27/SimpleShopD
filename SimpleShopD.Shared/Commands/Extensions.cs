using Microsoft.Extensions.DependencyInjection;
using SimpleShopD.Shared.Abstractions.Commands;
using System.Reflection;

namespace SimpleShopD.Shared.Commands
{
    public static class Extensions
    {
        public static IServiceCollection AddCommands(this IServiceCollection services)
        {
            var assembly = Assembly.GetCallingAssembly();

            services.AddSingleton<ICommandDispatcher, InMemoryCommandDispatcher>();
            services.Scan(s => s.FromAssemblies(assembly)
                .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            services.AddSingleton<ICommandTResultDispatcher, InMemoryCommandTResultDispatcher>();
            services.Scan(x => x.FromAssemblies(assembly)
                .AddClasses(y => y.AssignableTo(typeof(ICommandTResultHandler<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            return services;
        }
    }
}
