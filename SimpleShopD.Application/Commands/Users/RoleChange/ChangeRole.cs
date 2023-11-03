using SimpleShopD.Domain.Users.ValueObjects;
using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Commands.Users.RoleChange
{
    public sealed record ChangeRole(Guid UserId, Role NewRole) : ICommand;
}
