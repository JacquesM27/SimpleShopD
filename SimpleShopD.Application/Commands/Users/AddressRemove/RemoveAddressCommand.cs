using SimpleShopD.Application.Exceptions;
using SimpleShopD.Domain.Repositories;
using SimpleShopD.Domain.Services;
using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Commands.Users.AddressRemove
{
    internal sealed class RemoveAddressCommand : ICommandHandler<RemoveAddress>
    {
        private readonly IUserRepository _userRepository;
        private readonly IContextAccessor _contextAccessor;

        public RemoveAddressCommand(IUserRepository userRepository, IContextAccessor contextAccessor)
        {
            _userRepository = userRepository;
            _contextAccessor = contextAccessor;
        }

        public async Task HandleAsync(RemoveAddress command)
        {
            var userId = _contextAccessor.GetUserId();
            var user = await _userRepository.GetAsync(userId)
                ?? throw new UserDoesNotExistException(userId.ToString());

            user.RemoveAddress(command.AddressId);
            await _userRepository.UpdateAsync(user);
        }
    }
}
