namespace SimpleShopD.Domain.Shared.Exceptions
{
    public class InvalidFullnameLengthException : Exception
    {
        public InvalidFullnameLengthException(string message) : base(message) { }
    }
}
