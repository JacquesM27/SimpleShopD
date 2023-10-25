namespace SimpleShopD.Shared.Abstractions.Commands
{
    public interface ICommandTResultHandler<in TCommand, TResult> where TCommand : class, ICommandTResult
    {
        Task<TResult> HandleAsync(TCommand command);
    }
}
