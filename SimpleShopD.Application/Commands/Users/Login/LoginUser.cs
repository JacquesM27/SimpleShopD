using SimpleShopD.Domain.Services.DTO;
using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Commands.Users.Login
{
    public sealed record LoginUser(string Email, string Password) : ICommandTResult<AuthResponse>;
}
