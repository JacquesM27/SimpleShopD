using Microsoft.Extensions.Logging;
using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Infrastructure.Logging
{
    internal sealed class CommandHandlerLoggerDecorator<TCommand> : ICommandHandler<TCommand> 
        where TCommand : class, ICommand
    {
        private readonly ICommandHandler<TCommand> _handler;
        private readonly ILogger<CommandHandlerLoggerDecorator<TCommand>> _logger;

        public CommandHandlerLoggerDecorator(ICommandHandler<TCommand> handler, ILogger<CommandHandlerLoggerDecorator<TCommand>> logger)
        {
            _handler = handler;
            _logger = logger;
        }

        public async Task HandleAsync(TCommand command)
        {
            string commandName = command.GetType().Name;
            try
            {
                _logger.LogInformation($"Started processing {commandName} command.", command);
                await _handler.HandleAsync(command);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to process {commandName} command.", new object[] { command, ex });
                throw;
            }
            finally
            {
                _logger.LogInformation($"Finished processing {commandName} command.", command);
            }
        }
    }
}
