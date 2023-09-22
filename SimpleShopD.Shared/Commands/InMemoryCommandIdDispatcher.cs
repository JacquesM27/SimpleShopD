using Microsoft.Extensions.DependencyInjection;
using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Shared.Commands
{
    internal sealed class InMemoryCommandIdDispatcher : ICommandIdDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public InMemoryCommandIdDispatcher(IServiceProvider serviceProvider)
            => _serviceProvider = serviceProvider;

        public async Task DispatchAsync<TCommand, TId>(TCommand command) where TCommand : class, ICommand
        {
            using var scope = _serviceProvider.CreateScope();
            var handler = scope.ServiceProvider.GetRequiredService<ICommandIdHandler<TCommand, TId>>();

            await handler.HandleAsync(command);
        }
    }
}
