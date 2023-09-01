using SimpleShopD.Domain.Enum;
using SimpleShopD.Domain.Models.Shared.Base;
using SimpleShopD.Domain.Models.Users.ValueObjects;
using SimpleShopD.Domain.Services;
using SimpleShopD.Domain.Services.POCO;

namespace SimpleShopD.Domain.Models.Users
{
    internal sealed class User<T> : AggregateRoot<T> where T : notnull
    {
        public Fullname Fullname { get; }
        public Email Email { get; }
        public Password Password { get; private set; }
        public Token? RefreshToken { get; private set; }
        public Token? ActivationToken { get; private set; }
        public Token? ResetPasswordToken { get; private set; }
        public Status Status { get; private set; }
        public Role Role { get; }

        public User(T id, Fullname fullname, Email email, Password password, Role role) : base(id)
        {
            Fullname = fullname;
            Email = email;
            Password = password;
            ActivationToken = TokenType.Activation;
            Status = AccountStatus.Inactive;
            Role = role;
        }

        public void GenerateResetPasswordToken() => 
            ResetPasswordToken = TokenType.ResetPassword;

        public void SetNewPassword(string password) =>
            Password = password;

        public void GenerateRefreshToken() =>
            RefreshToken = TokenType.Refresh;

        public void Activate()
        {
            Status = AccountStatus.Active;
            ActivationToken = null;
        }

        public LoginToken Login(ITokenProvider<T> tokenProvider)//TODO : Where I should store validation?
        {
            //validation?
            RefreshToken = TokenType.Refresh;
            return tokenProvider.Provide(RefreshToken.Value.ExpirationDate, "user", Id);
        }
    }
}
