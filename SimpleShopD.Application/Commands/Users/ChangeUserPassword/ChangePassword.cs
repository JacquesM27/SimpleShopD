using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Commands.Users.ChangeUserPassword
{
    public sealed record ChangePassword(string OldPassword, string NewPassword) : ICommand;
}
