using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Commands.Users.NewPassword
{
    public sealed record SetNewPassword(Guid UserId, string NewPassword, string ResetPasswordToken) : ICommand;
}
