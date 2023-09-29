using SimpleShopD.Domain.Enum;
using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Commands.Users.RoleChange
{
    public sealed record ChangeRole(Guid UserId, UserRole NewRole) : ICommand;
}
