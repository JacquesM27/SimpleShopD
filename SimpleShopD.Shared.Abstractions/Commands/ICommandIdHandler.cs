namespace SimpleShopD.Shared.Abstractions.Commands
{
    public interface ICommandIdHandler<in TCommand, TId> where TCommand : class, ICommand
    {
        Task<TId> HandleAsync(TCommand command);
    }
}
