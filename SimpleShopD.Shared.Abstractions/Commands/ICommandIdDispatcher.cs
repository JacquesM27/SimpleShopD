namespace SimpleShopD.Shared.Abstractions.Commands
{
    public interface ICommandIdDispatcher
    {
        Task DispatchAsync<TCommand, TId>(TCommand command) where TCommand : class, ICommand;
    }
}
