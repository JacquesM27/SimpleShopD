using SimpleShopD.Shared.Abstractions.Exceptions;
namespace SimpleShopD.Domain.Users.Exceptions
{
    public class PasswordPolicyException : CustomException
    {
        public PasswordPolicyException(string message) : base(message) { }
    }
}
