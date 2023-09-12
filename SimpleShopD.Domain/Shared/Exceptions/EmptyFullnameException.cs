namespace SimpleShopD.Domain.Shared.Exceptions
{
    public class EmptyFullnameException : Exception
    {
        public EmptyFullnameException(string message) : base(message) { }
    }
}
