using SimpleShopD.Application.Commands.SharedDto;
using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Commands.Users.UserRegister
{
    public sealed record RegisterUser(string Firstname, string Lastname, string Email, string Password, IEnumerable<Address> Addresses) : ICommand;
}
