using SimpleShopD.Application.Commands.SharedDto;
using SimpleShopD.Application.Exceptions;
using SimpleShopD.Domain.Repositories;
using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Commands.Users.AddressAdd
{
    internal sealed class AddAddressCommand : ICommandHandler<AddAddress>
    {
        private readonly IUserRepository _userRepository;

        public AddAddressCommand(IUserRepository userRepository) 
            => _userRepository = userRepository;

        public async Task HandleAsync(AddAddress command)
        {
            var user = await _userRepository.GetAsync(command.UserId)
                ?? throw new UserDoesNotExistException(command.UserId.ToString());

            user.AddAddress(command.Address.MapToDomainAddress());
            await _userRepository.UpdateAsync(user);
        }
    }
}
