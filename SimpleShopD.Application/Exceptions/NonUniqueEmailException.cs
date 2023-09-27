using SimpleShopD.Shared.Abstractions.Exceptions;

namespace SimpleShopD.Application.Exceptions
{
    public sealed class NonUniqueEmailException : CustomException
    {
        public NonUniqueEmailException(string message) : base(message)
        {
        }
    }
}
