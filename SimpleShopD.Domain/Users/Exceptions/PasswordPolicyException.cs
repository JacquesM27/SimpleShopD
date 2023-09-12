namespace SimpleShopD.Domain.Users.Exceptions
{
    public class PasswordPolicyException : Exception
    {
        public PasswordPolicyException(string message) : base(message) { }
    }
}
