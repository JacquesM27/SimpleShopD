using SimpleShopD.Application.Exceptions;
using SimpleShopD.Domain.Repositories;
using SimpleShopD.Domain.Services;
using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Commands.Users.ChangeUserPassword
{
    internal sealed class ChangePasswordCommand : ICommandHandler<ChangePassword>
    {
        private readonly IUserRepository _userRepository;
        private readonly IContextAccessor _contextAccessor;

        public ChangePasswordCommand(IUserRepository userRepository, IContextAccessor contextAccessor)
        {
            _userRepository = userRepository;
            _contextAccessor = contextAccessor;
        }

        public async Task HandleAsync(ChangePassword command)
        {
            var userId = _contextAccessor.GetUserId();
            var user = await _userRepository.GetAsync(userId)
                ?? throw new UserDoesNotExistException(userId.ToString());

            user.ChangePassword(command.OldPassword, command.NewPassword);
            await _userRepository.UpdateAsync(user);
        }
    }
}
