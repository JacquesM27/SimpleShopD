using SimpleShopD.Application.Exceptions;
using SimpleShopD.Domain.Repositories;
using SimpleShopD.Domain.Services;
using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Commands.Users.Logout
{
    public sealed class LogoutUserHandler : ICommandHandler<LogoutUser>
    {
        private readonly IUserRepository _userRepository;
        private readonly IContextAccessor _contextAccessor;

        public LogoutUserHandler(IUserRepository userRepository, IContextAccessor contextAccessor)
        {
            _userRepository = userRepository;
            _contextAccessor = contextAccessor;
        }

        public async Task HandleAsync(LogoutUser command)
        {
            var userId = _contextAccessor.GetUserId();
            var user = await _userRepository.GetAsync(userId)
                ?? throw new UserDoesNotExistException(userId.ToString());

            user.LogOut(_contextAccessor);
            await _userRepository.UpdateAsync(user);
        }
    }
}
