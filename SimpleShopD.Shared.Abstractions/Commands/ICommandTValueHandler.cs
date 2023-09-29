namespace SimpleShopD.Shared.Abstractions.Commands
{
    public interface ICommandTValueHandler<in TCommand, TValue> where TCommand : class, ICommand
    {
        Task<TValue> HandleAsync(TCommand command);
    }
}
