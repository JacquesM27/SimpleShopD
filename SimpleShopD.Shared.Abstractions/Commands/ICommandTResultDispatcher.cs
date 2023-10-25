namespace SimpleShopD.Shared.Abstractions.Commands
{
    public interface ICommandTResultDispatcher
    {
        Task<TResult> DispatchAsync<TResult>(ICommandTResult<TResult> command);
    }
}
