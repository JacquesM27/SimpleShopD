using SimpleShopD.Shared.Abstractions.Exceptions;
namespace SimpleShopD.Domain.Shared.Exceptions
{
    public class EmptyFullnameException : CustomException
    {
        public EmptyFullnameException(string message) : base(message) { }
    }
}
