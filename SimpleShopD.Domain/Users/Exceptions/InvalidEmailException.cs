using SimpleShopD.Shared.Abstractions.Exceptions;
namespace SimpleShopD.Domain.Users.Exceptions
{
    public class InvalidEmailException : CustomException
    {
        public InvalidEmailException(string message) : base(message) { }
    }
}
