using SimpleShopD.Shared.Abstractions.Exceptions;
namespace SimpleShopD.Domain.Shared.Exceptions
{
    public class InvalidFullnameLengthException : CustomException
    {
        public InvalidFullnameLengthException(string message) : base(message) { }
    }
}
