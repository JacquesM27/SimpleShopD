using SimpleShopD.Application.Exceptions;
using SimpleShopD.Domain.Repositories;
using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Commands.Users.RefreshToken
{
    internal sealed class GenerateRefreshTokenCommand : ICommandHandler<GenerateRefreshToken>
    {
        private readonly IUserRepository _userRepository;

        public GenerateRefreshTokenCommand(IUserRepository userRepository) 
            => _userRepository = userRepository;

        public async Task HandleAsync(GenerateRefreshToken command)
        {
            var user = await _userRepository.GetAsync(command.UserId)
                ?? throw new UserDoesNotExistException(command.UserId.ToString());

            user.GenerateRefreshToken();
            await _userRepository.UpdateAsync(user);
        }
    }
}
