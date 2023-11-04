using SimpleShopD.Application.Exceptions;
using SimpleShopD.Domain.Repositories;
using SimpleShopD.Domain.Services;
using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Commands.Users.AddressAdd
{
    internal sealed class AddAddressCommand : ICommandTResultHandler<AddAddress, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly IContextAccessor _contextAccessor;

        public AddAddressCommand(IUserRepository userRepository, IContextAccessor contextAccessor)
        {
            _userRepository = userRepository;
            _contextAccessor = contextAccessor;
        }

        public async Task<Guid> HandleAsync(AddAddress command)
        {
            var userId = _contextAccessor.GetUserId();
            var user = await _userRepository.GetAsync(userId)
                ?? throw new UserDoesNotExistException(userId.ToString());

            var guid = Guid.NewGuid();
            user.AddAddress(guid, command.Address.Country, command.Address.City, command.Address.ZipCode, command.Address.Street, command.Address.BuildingNumber);
            await _userRepository.UpdateAsync(user);
            return guid;
        }
    }
}
