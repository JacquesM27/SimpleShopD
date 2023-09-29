namespace SimpleShopD.Shared.Abstractions.Commands
{
    public interface ICommandTValueDispatcher
    {
        Task DispatchAsync<TCommand, TValue>(TCommand command) where TCommand : class, ICommand;
    }
}
