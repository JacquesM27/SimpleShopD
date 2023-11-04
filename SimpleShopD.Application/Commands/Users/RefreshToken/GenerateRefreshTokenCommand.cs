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
        private readonly IContextAccessor _contextAccessor;

        public GenerateRefreshTokenCommand(IUserRepository userRepository, ITokenProvider tokenProvider, IContextAccessor cookieTokenAccessor)
        {
            _userRepository = userRepository;
            _tokenProvider = tokenProvider;
            _contextAccessor = cookieTokenAccessor;
        }

        public async Task<AuthToken> HandleAsync(GenerateRefreshToken command)
        {
            var userId = _contextAccessor.GetUserId();
            if (userId == Guid.Empty)
                throw new UserDoesNotExistException(userId.ToString());
            var user = await _userRepository.GetAsync(userId)
                ?? throw new UserDoesNotExistException(userId.ToString());

            var token = user.GenerateRefreshToken(_tokenProvider, _contextAccessor);
            await _userRepository.UpdateAsync(user);
            return token;
        }
    }
}
