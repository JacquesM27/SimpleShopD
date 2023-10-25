using Microsoft.Extensions.Logging;
using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Infrastructure.Logging
{
    internal sealed class CommandTValueHandlerLoggerDecorator<TCommand, TValue> : ICommandTResultHandler<TCommand, TValue>
        where TCommand : class, ICommandTResult
    {
        private readonly ICommandTResultHandler<TCommand, TValue> _handler;
        private readonly ILogger<CommandTValueHandlerLoggerDecorator<TCommand, TValue>> _logger;

        public CommandTValueHandlerLoggerDecorator(ICommandTResultHandler<TCommand, TValue> handler, ILogger<CommandTValueHandlerLoggerDecorator<TCommand, TValue>> logger)
        {
            _handler = handler;
            _logger = logger;
        }

        public async Task<TValue> HandleAsync(TCommand command)
        {
            string commandName = command.GetType().Name;
            try
            {
                _logger.LogInformation($"Started processing {commandName} command.", command);
                var result = await _handler.HandleAsync(command);
                return result;
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
