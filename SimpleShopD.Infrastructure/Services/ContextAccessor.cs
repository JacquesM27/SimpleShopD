using Microsoft.AspNetCore.Http;
using SimpleShopD.Domain.Services;

namespace SimpleShopD.Infrastructure.Services
{
    public sealed class ContextAccessor : IContextAccessor
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public ContextAccessor(IHttpContextAccessor contextAccessor) 
            => _contextAccessor = contextAccessor;

        public void AppendRefreshToken(string refreshToken, DateTime expires)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = expires,
                SameSite = SameSiteMode.None,
                Secure = true
            };
            _contextAccessor?.HttpContext?.Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }

        public string? GetRefreshToken()
            => _contextAccessor?.HttpContext?.Request.Cookies["refreshToken"];

        public Guid GetUserId()
            => string.IsNullOrEmpty(_contextAccessor.HttpContext?.User?.Identity?.Name) ? Guid.Empty : Guid.Parse(_contextAccessor!.HttpContext!.User!.Identity!.Name!);
    }
}
