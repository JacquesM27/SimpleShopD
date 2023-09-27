using SimpleShopD.Application.Commands.SharedDto;
using SimpleShopD.Application.Exceptions;
using SimpleShopD.Domain.Repositories;
using SimpleShopD.Domain.Users;
using SimpleShopD.Shared.Abstractions.Commands;
using DomainVo = SimpleShopD.Domain.Shared.ValueObjects;

namespace SimpleShopD.Application.Commands.Users.UserRegister
{
    internal sealed class RegisterUserCommand : ICommandIdHandler<RegisterUser, Guid>
    {
        private readonly IUserRepository _userRepository;

        public RegisterUserCommand(IUserRepository userRepository) 
            => _userRepository = userRepository;

        public async Task<Guid> HandleAsync(RegisterUser command)
        {
            if (! await _userRepository.IsTheEmailUniqueAsync(command.Email))
                throw new NonUniqueEmailException(command.Email);

            var addresses = MapAddresses(command.Addresses);
            var newUser = User<Guid>.RegisterUser(Guid.NewGuid(), MapFullname(command), command.Email, command.Password, addresses);
            await _userRepository.AddAsync(newUser);
            return newUser.Id;
        }

       
        private static HashSet<DomainVo.Address> MapAddresses(IEnumerable<Address> addresses)
        {
            HashSet<DomainVo.Address> mappedAddresses = new();
            foreach (var address in addresses)
            {
                mappedAddresses.Add(address.MapToDomainAddress());
            }
            return mappedAddresses;
        }

        private static DomainVo.Fullname MapFullname(RegisterUser command)
            => new(command.Firstname, command.Lastname);
    }
}
