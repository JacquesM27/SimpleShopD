namespace SimpleShopD.Domain.Shared.Exceptions
{
    public class EmptyAddressParameterException : Exception
    {
        public EmptyAddressParameterException(string message) : base(message) { }
    }
}
