using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Commands.Users.ResetPasswordToken
{
    public sealed record GeneratePasswordResetToken(Guid UserId) : ICommand;
}
