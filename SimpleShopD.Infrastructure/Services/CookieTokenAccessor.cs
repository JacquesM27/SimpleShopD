using Microsoft.AspNetCore.Http;
using SimpleShopD.Domain.Services;

namespace SimpleShopD.Infrastructure.Services
{
    public sealed class CookieTokenAccessor : ICookieTokenAccessor
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public CookieTokenAccessor(IHttpContextAccessor contextAccessor) 
            => _contextAccessor = contextAccessor;

        public void AppendRefreshToken(string refreshToken, DateTime expires)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = expires,
            };
            _contextAccessor?.HttpContext?.Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }

        public string? GetRefreshToken()
        {
            var refreshToken = _contextAccessor?.HttpContext?.Request.Cookies["refreshToken"];
            return refreshToken;
        }
    }
}
