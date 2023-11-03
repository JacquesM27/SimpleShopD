using SimpleShopD.Application.Dto;
using SimpleShopD.Domain.Users.ValueObjects;
using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Commands.Users.UserRegister
{
    public sealed record RegisterUser(string Firstname, string Lastname, string Email, 
        string Password, Role UserRole, IEnumerable<AddressDto> Addresses, string Role, Guid? AuthorId) : ICommandTResult<Guid>;
}
