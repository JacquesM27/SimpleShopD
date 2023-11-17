﻿namespace SimpleShopD.Domain.Services
{
    public interface IContextAccessor
    {
        string? GetRefreshToken();

        void AppendRefreshToken(string refreshToken, DateTime expires);

        void RemoveRefreshToken();

        Guid GetUserId();
    }
}
