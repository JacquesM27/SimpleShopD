using SimpleShopD.Application.Exceptions;
using SimpleShopD.Domain.Repositories;
using SimpleShopD.Domain.Services;
using SimpleShopD.Domain.Services.DTO;
using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Commands.Users.Login
{
    internal sealed class LoginUserCommand : ICommandTResultHandler<LoginUser, AuthResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenProvider _tokenProvider;
        private readonly IContextAccessor _contextAccessor;

        public LoginUserCommand(IUserRepository userRepository, ITokenProvider tokenProvider, IContextAccessor cookieTokenAccessor)
        {
            _userRepository = userRepository;
            _tokenProvider = tokenProvider;
            _contextAccessor = cookieTokenAccessor;
        }

        public async Task<AuthResponse> HandleAsync(LoginUser command)
        {
            var user = await _userRepository.GetByEmailAsync(command.Email)
                ?? throw new UserDoesNotExistException(command.Email);

            var token = user.Login(command.Password, _tokenProvider, _contextAccessor);
            await _userRepository.UpdateAsync(user);
            return token;
        }
    }
}
