using SimpleShopD.Application.Commands.SharedDto;
using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Commands.Users.AddressAdd
{
    public sealed record AddAddress(Guid UserId, Address Address) : ICommand;
}
