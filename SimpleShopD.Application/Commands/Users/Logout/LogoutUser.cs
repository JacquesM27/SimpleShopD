using SimpleShopD.Shared.Abstractions.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleShopD.Application.Commands.Users.Logout
{
    public sealed record LogoutUser() : ICommand;
}
