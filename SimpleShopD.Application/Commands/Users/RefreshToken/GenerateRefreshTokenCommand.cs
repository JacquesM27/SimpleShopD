using SimpleShopD.Application.Exceptions;
using SimpleShopD.Domain.Repositories;
using SimpleShopD.Domain.Services;
using SimpleShopD.Domain.Services.DTO;
using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Commands.Users.RefreshToken
{
    internal sealed class GenerateRefreshTokenCommand : ICommandTResultHandler<GenerateRefreshToken, AuthToken>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenProvider _tokenProvider;
        private readonly ICookieTokenAccessor _cookieTokenAccessor;

        public GenerateRefreshTokenCommand(IUserRepository userRepository, ITokenProvider tokenProvider, ICookieTokenAccessor cookieTokenAccessor)
        {
            _userRepository = userRepository;
            _tokenProvider = tokenProvider;
            _cookieTokenAccessor = cookieTokenAccessor;
        }

        public async Task<AuthToken> HandleAsync(GenerateRefreshToken command)
        {
            var user = await _userRepository.GetAsync(command.UserId)
                ?? throw new UserDoesNotExistException(command.UserId.ToString());

            var token = user.GenerateRefreshToken(_tokenProvider, _cookieTokenAccessor);
            await _userRepository.UpdateAsync(user);
            return token;
        }
    }
}
