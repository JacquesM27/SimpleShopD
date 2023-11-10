﻿using SimpleShopD.Domain.Services.DTO;
using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Commands.Users.RefreshToken
{
    public sealed record GenerateRefreshToken() : ICommandTResult<AuthResponse>;
}
