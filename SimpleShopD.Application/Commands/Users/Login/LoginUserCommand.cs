using SimpleShopD.Application.Exceptions;
using SimpleShopD.Domain.Repositories;
using SimpleShopD.Domain.Services;
using SimpleShopD.Domain.Services.DTO;
using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Commands.Users.Login
{
    internal sealed class LoginUserCommand : ICommandTResultHandler<LoginUser, AuthToken>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenProvider _tokenProvider;
        private readonly ICookieTokenAccessor _cookieTokenAccessor;

        public LoginUserCommand(IUserRepository userRepository, ITokenProvider tokenProvider, ICookieTokenAccessor cookieTokenAccessor)
        {
            _userRepository = userRepository;
            _tokenProvider = tokenProvider;
            _cookieTokenAccessor = cookieTokenAccessor;
        }

        public async Task<AuthToken> HandleAsync(LoginUser command)
        {
            var user = await _userRepository.GetByEmailAsync(command.Email)
                ?? throw new UserDoesNotExistException(command.Email);

            var token = user.Login(command.Password, _tokenProvider, _cookieTokenAccessor);
            await _userRepository.UpdateAsync(user);
            return token;
        }
    }
}
