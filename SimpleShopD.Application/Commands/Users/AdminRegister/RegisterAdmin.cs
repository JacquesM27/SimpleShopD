using SimpleShopD.Application.Commands.SharedDto;
using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Commands.Users.AdminRegister
{
    public sealed record RegisterAdmin(Guid AdminCreatorId, string Firstname, string Lastname, string Email, string Password, IEnumerable<Address> Addresses) : ICommand;
}
