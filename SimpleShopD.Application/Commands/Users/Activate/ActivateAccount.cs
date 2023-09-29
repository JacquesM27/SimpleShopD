using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Commands.Users.Activate
{
    public sealed record ActivateAccount(Guid UserId, string ActivationToken) : ICommand;
}
