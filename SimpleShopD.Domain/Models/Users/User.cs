using SimpleShopD.Domain.Enum;
using SimpleShopD.Domain.Models.Shared.Base;
using SimpleShopD.Domain.Models.Shared.ValueObjects;
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
        public Role Role { get; private set; }
        public HashSet<Address> Addresses { get; private set; }

        public User(T id, Fullname fullname, Email email, Password password, Role role, HashSet<Address> addresses) : base(id)
        {
            Fullname = fullname;
            Email = email;
            Password = password;
            ActivationToken = TokenType.Activation;
            Status = AccountStatus.Inactive;
            Role = role;
            Addresses = addresses;
        }

        public void GenerateResetPasswordToken() => 
            ResetPasswordToken = TokenType.ResetPassword;

        public void SetNewPassword(string newPassword, string resetPasswordToken)
        {
            if(ResetPasswordToken is null)
                throw new ArgumentException("Missing reset password token");
            if (resetPasswordToken != ResetPasswordToken.Value.Value)
                throw new ArgumentException("Invalid reset password token");
            Password = new Password(newPassword);
        }

        public void ChangePassword(string oldPassword, string newPassword)
        {
            if(!Password.VerifyPassword(oldPassword))
                throw new ArgumentException("Invalid old password");
            Password = new Password(newPassword);
        }

        public void GenerateRefreshToken() =>
            RefreshToken = TokenType.Refresh;

        public void Activate(string activationToken)
        {
            if(ActivationToken is null)
                throw new ArgumentException("Missing activation token");
            if (activationToken != ActivationToken.Value.Value)
                throw new ArgumentException("Invalid activation token");
            Status = AccountStatus.Active;
            ActivationToken = null;
        }

        public LoginToken Login(string password, ITokenProvider<T> tokenProvider)
        {
            if(!Password.VerifyPassword(password))
                throw new ArgumentException("Invalid password");
            RefreshToken = TokenType.Refresh;
            return tokenProvider.Provide(RefreshToken.Value.ExpirationDate, Role.UserRole, Id);
        }

        public void ChangeRole(UserRole userRole) =>
            Role = userRole;

        public void AddAddress(Address address) =>
            Addresses.Add(address);

        public void RemoveAddress(Address address) =>
            Addresses.Remove(address);
    }
}
