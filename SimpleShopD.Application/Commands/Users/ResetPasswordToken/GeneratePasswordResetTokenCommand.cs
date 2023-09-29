using SimpleShopD.Application.Exceptions;
using SimpleShopD.Domain.Repositories;
using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Commands.Users.ResetPasswordToken
{
    internal sealed class GeneratePasswordResetTokenCommand : ICommandHandler<GeneratePasswordResetToken>
    {
        private readonly IUserRepository _userRepository;

        public GeneratePasswordResetTokenCommand(IUserRepository userRepository) 
            => _userRepository = userRepository;

        public async Task HandleAsync(GeneratePasswordResetToken command)
        {
            var user = await _userRepository.GetAsync(command.UserId)
                ?? throw new UserDoesNotExistException(command.UserId.ToString());

            user.GeneratePasswordResetToken();
            await _userRepository.UpdateAsync(user);
        }
    }
}
