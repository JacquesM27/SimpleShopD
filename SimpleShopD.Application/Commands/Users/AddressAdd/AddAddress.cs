using SimpleShopD.Application.Dto;
using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Commands.Users.AddressAdd
{
    public sealed record AddAddress(Guid UserId, AddressDto Address) : ICommandTResult<Guid>;
}
