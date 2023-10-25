using SimpleShopD.Application.Commands.SharedDto;
using SimpleShopD.Domain.Enum;
using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Commands.Users.UserRegister
{
    public sealed record RegisterUser(string Firstname, string Lastname, string Email, 
        string Password, UserRole UserRole, IEnumerable<Address> Addresses, string Role, Guid? AuthorId) : ICommandTResult<Guid>;
}
