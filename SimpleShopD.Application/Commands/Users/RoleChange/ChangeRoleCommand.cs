using SimpleShopD.Application.Exceptions;
using SimpleShopD.Domain.Repositories;
using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Commands.Users.RoleChange
{
    internal sealed class ChangeRoleCommand : ICommandHandler<ChangeRole>
    {
        private readonly IUserRepository _userRepository;

        public ChangeRoleCommand(IUserRepository userRepository) 
            => _userRepository = userRepository;

        public async Task HandleAsync(ChangeRole command)
        {
            var user = await _userRepository.GetAsync(command.UserId)
                ?? throw new UserDoesNotExistException(command.UserId.ToString());

            user.ChangeRole(command.NewRole);
            await _userRepository.UpdateAsync(user);
        }
    }
}
