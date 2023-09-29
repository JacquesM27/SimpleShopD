using SimpleShopD.Application.Exceptions;
using SimpleShopD.Domain.Repositories;
using SimpleShopD.Domain.Services;
using SimpleShopD.Domain.Services.DTO;
using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Commands.Users.Login
{
    internal sealed class LoginUserCommand : ICommandTValueHandler<LoginUser, LoginToken>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenProvider _tokenProvider;

        public LoginUserCommand(IUserRepository userRepository, ITokenProvider tokenProvider)
        {
            _userRepository = userRepository;
            _tokenProvider = tokenProvider;
        }

        public async Task<LoginToken> HandleAsync(LoginUser command)
        {
            var user = await _userRepository.GetByEmailAsync(command.Email)
                ?? throw new UserDoesNotExistException(command.Email);

            var token = user.Login(command.Password, _tokenProvider);
            await _userRepository.UpdateAsync(user);
            return token;
        }
    }
}
