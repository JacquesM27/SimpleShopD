using SimpleShopD.Shared.Abstractions.Exceptions;
namespace SimpleShopD.Domain.Users.Exceptions
{
    public class ChangingPasswordException : CustomException
    {
        public ChangingPasswordException(string message) : base(message) { }
    }
}
