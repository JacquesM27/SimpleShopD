using SimpleShopD.Application.Exceptions;
using SimpleShopD.Domain.Repositories;
using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Commands.Users.AddressRemove
{
    internal sealed class RemoveAddressCommand : ICommandHandler<RemoveAddress>
    {
        private readonly IUserRepository _userRepository;

        public RemoveAddressCommand(IUserRepository userRepository) 
            => _userRepository = userRepository;

        public async Task HandleAsync(RemoveAddress command)
        {
            var user = await _userRepository.GetAsync(command.UserId)
                ?? throw new UserDoesNotExistException(command.UserId.ToString());

            user.RemoveAddress(command.AddressId);
            await _userRepository.UpdateAsync(user);
        }
    }
}
