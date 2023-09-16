using SimpleShopD.Shared.Abstractions.Exceptions;
namespace SimpleShopD.Domain.Users.Exceptions
{
    public class LoginOperationException : CustomException
    {
        public LoginOperationException(string message) : base(message) { }
    }
}
