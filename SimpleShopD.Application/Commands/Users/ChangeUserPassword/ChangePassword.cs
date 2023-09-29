using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Commands.Users.ChangeUserPassword
{
    public sealed record ChangePassword(Guid UserId, string OldPassword, string NewPassword) : ICommand;
}
