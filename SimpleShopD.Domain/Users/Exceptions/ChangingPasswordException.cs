namespace SimpleShopD.Domain.Users.Exceptions
{
    public class ChangingPasswordException : Exception
    {
        public ChangingPasswordException(string message) : base(message) { }
    }
}
