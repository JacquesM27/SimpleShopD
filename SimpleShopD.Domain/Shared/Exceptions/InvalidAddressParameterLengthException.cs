namespace SimpleShopD.Domain.Shared.Exceptions
{
    public class InvalidAddressParameterLengthException : Exception
    {
        public InvalidAddressParameterLengthException(string message) : base(message) { }
    }
}
