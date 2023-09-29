using SimpleShopD.Application.Exceptions;
using SimpleShopD.Domain.Repositories;
using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Commands.Users.Activate
{
    internal sealed class ActivateAccountCommand : ICommandHandler<ActivateAccount>
    {
        private readonly IUserRepository _userRepository;

        public ActivateAccountCommand(IUserRepository userRepository) 
            => _userRepository = userRepository;

        public async Task HandleAsync(ActivateAccount command)
        {
            var user = await _userRepository.GetAsync(command.UserId)
                ?? throw new UserDoesNotExistException(command.UserId.ToString());

            user.Activate(command.ActivationToken);
            await _userRepository.UpdateAsync(user);
        }
    }
}
