using SimpleShopD.Application.Exceptions;
using SimpleShopD.Domain.Repositories;
using SimpleShopD.Domain.Services;
using SimpleShopD.Domain.Services.DTO;
using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Commands.Users.RefreshToken
{
    internal sealed class GenerateRefreshTokenCommand(
        IUserRepository userRepository, 
        ITokenProvider tokenProvider,
        IContextAccessor cookieTokenAccessor)
            //cool feature as long as you don't change anything in the default constructor
            : ICommandTResultHandler<GenerateRefreshToken, AuthResponse>
    {
        public async Task<AuthResponse> HandleAsync(GenerateRefreshToken command)
        {
            var userRefresh = cookieTokenAccessor.GetRefreshToken();
            if (string.IsNullOrEmpty(userRefresh))
                throw new MissingRefreshTokenException(string.Empty);
            var user = await userRepository.GetByRefresh(userRefresh)
                ?? throw new UserDoesNotExistException(userRefresh);

            var token = user.GenerateRefreshToken(tokenProvider, cookieTokenAccessor);
            await userRepository.UpdateAsync(user);
            return token;
        }
    }
}
