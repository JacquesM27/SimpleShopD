using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Commands.Users.AddressRemove
{
    public sealed record RemoveAddress(Guid UserId, Guid AddressId) : ICommand;
}
