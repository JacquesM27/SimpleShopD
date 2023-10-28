using SimpleShopD.Application.Dto;
using SimpleShopD.Domain.Enum;
using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Commands.Users.UserRegister
{
    public sealed record RegisterUser(string Firstname, string Lastname, string Email, 
        string Password, UserRole UserRole, IEnumerable<AddressDto> Addresses, string Role, Guid? AuthorId) : ICommandTResult<Guid>;
}
