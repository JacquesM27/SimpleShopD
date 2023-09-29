using Microsoft.Extensions.DependencyInjection;
using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Shared.Commands
{
    internal sealed class InMemoryCommandTValueDispatcher : ICommandTValueDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public InMemoryCommandTValueDispatcher(IServiceProvider serviceProvider)
            => _serviceProvider = serviceProvider;

        public async Task DispatchAsync<TCommand, TValue>(TCommand command) where TCommand : class, ICommand
        {
            using var scope = _serviceProvider.CreateScope();
            var handler = scope.ServiceProvider.GetRequiredService<ICommandTValueHandler<TCommand, TValue>>();

            await handler.HandleAsync(command);
        }
    }
}
