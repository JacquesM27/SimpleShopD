namespace SimpleShopD.Domain.Users.Exceptions
{
    public class LoginOperationException : Exception
    {
        public LoginOperationException(string message) : base(message) { }
    }
}
