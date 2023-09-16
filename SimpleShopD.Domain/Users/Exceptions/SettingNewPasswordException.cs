using SimpleShopD.Shared.Abstractions.Exceptions;
namespace SimpleShopD.Domain.Users.Exceptions
{
    public class SettingNewPasswordException : CustomException
    {
        public SettingNewPasswordException(string message) : base(message) { }
    }
}
