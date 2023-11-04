using SimpleShopD.Application.Exceptions;
using SimpleShopD.Domain.Repositories;
using SimpleShopD.Domain.Services;
using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Commands.Users.ResetPasswordToken
{
    internal sealed class GeneratePasswordResetTokenCommand : ICommandHandler<GeneratePasswordResetToken>
    {
        private readonly IUserRepository _userRepository;
        private readonly IContextAccessor _contextAccessor;

        public GeneratePasswordResetTokenCommand(IUserRepository userRepository, IContextAccessor contextAccessor)
        {
            _userRepository = userRepository;
            _contextAccessor = contextAccessor;
        }

        public async Task HandleAsync(GeneratePasswordResetToken command)
        {
            var userId = _contextAccessor.GetUserId();
            var user = await _userRepository.GetAsync(userId)
                ?? throw new UserDoesNotExistException(userId.ToString());

            user.GeneratePasswordResetToken();
            await _userRepository.UpdateAsync(user);
        }
    }
}
