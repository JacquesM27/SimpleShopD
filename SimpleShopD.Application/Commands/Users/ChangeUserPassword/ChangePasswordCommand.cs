using SimpleShopD.Application.Exceptions;
using SimpleShopD.Domain.Repositories;
using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Commands.Users.ChangeUserPassword
{
    internal sealed class ChangePasswordCommand : ICommandHandler<ChangePassword>
    {
        private readonly IUserRepository _userRepository;

        public ChangePasswordCommand(IUserRepository userRepository) 
            => _userRepository = userRepository;

        public async Task HandleAsync(ChangePassword command)
        {
            var user = await _userRepository.GetAsync(command.UserId)
                ?? throw new UserDoesNotExistException(command.UserId.ToString());

            user.ChangePassword(command.OldPassword, command.NewPassword);
            await _userRepository.UpdateAsync(user);
        }
    }
}
