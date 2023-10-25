using Microsoft.Extensions.DependencyInjection;
using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Shared.Commands
{
    internal sealed class InMemoryCommandTResultDispatcher : ICommandTResultDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public InMemoryCommandTResultDispatcher(IServiceProvider serviceProvider)
            => _serviceProvider = serviceProvider;

        public async Task<TResult> DispatchAsync<TResult>(ICommandTResult<TResult> command)
        {
            using var scope = _serviceProvider.CreateAsyncScope();
            var handlerType = typeof(ICommandTResultHandler<,>).MakeGenericType(command.GetType(), typeof(TResult));
            var handler = scope.ServiceProvider.GetRequiredService(handlerType);
            return await (Task<TResult>)handlerType.GetMethod(nameof(ICommandTResultHandler<ICommandTResult<TResult>, TResult>.HandleAsync))?
                .Invoke(handler, new[] { command });
        }
    }
}
