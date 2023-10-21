using SimpleShopD.Application.Exceptions;
using SimpleShopD.Domain.Repositories;
using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Commands.Users.AddressAdd
{
    internal sealed class AddAddressCommand : ICommandTValueHandler<AddAddress, Guid>
    {
        private readonly IUserRepository _userRepository;

        public AddAddressCommand(IUserRepository userRepository) 
            => _userRepository = userRepository;

        public async Task<Guid> HandleAsync(AddAddress command)
        {
            var user = await _userRepository.GetAsync(command.UserId)
                ?? throw new UserDoesNotExistException(command.UserId.ToString());

            var guid = Guid.NewGuid();
            user.AddAddress(guid, command.Address.Country, command.Address.City, command.Address.ZipCode, command.Address.Street, command.Address.BuildingNumber);
            await _userRepository.UpdateAsync(user);
            return guid;
        }
    }
}
