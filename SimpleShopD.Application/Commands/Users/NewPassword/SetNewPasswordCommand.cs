using SimpleShopD.Application.Exceptions;
using SimpleShopD.Domain.Repositories;
using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Commands.Users.NewPassword
{
    internal sealed class SetNewPasswordCommand : ICommandHandler<SetNewPassword>
    {
        private readonly IUserRepository _userRepository;

        public SetNewPasswordCommand(IUserRepository userRepository) 
            => _userRepository = userRepository;

        public async Task HandleAsync(SetNewPassword command)
        {
            var user = await _userRepository.GetAsync(command.UserId)
                ?? throw new UserDoesNotExistException(command.UserId.ToString());

            user.SetNewPassword(command.NewPassword, command.ResetPasswordToken);
            await _userRepository.UpdateAsync(user);
        }
    }
}
