using SimpleShopD.Domain.Enum;
using SimpleShopD.Domain.Services;
using SimpleShopD.Domain.Services.DTO;
using SimpleShopD.Domain.Shared.Base;
using SimpleShopD.Domain.Shared.ValueObjects;
using SimpleShopD.Domain.Users.Exceptions;
using SimpleShopD.Domain.Users.ValueObjects;

namespace SimpleShopD.Domain.Users
{
    public sealed class User : AggregateRoot<Guid>
    {
        public Fullname Fullname { get; }
        public Email Email { get; }
        public Password Password { get; private set; }
        public StatusOfAccount Status { get; private set; }
        public RoleOfUser RoleOfUser { get; private set; }
        public HashSet<Address> Addresses { get; private set; }
        public Token? ActivationToken { get; private set; }
        public Token? RefreshToken { get; private set; }
        public Token? ResetPasswordToken { get; private set; }

        public User(Guid id, Fullname fullname, Email email, Password password,
            RoleOfUser roleOfUser, HashSet<Address> addresses)
            : base(id)
        {
            Fullname = fullname;
            Email = email;
            Password = password;
            Status = AccountStatus.Inactive;
            RoleOfUser = roleOfUser;
            Addresses = addresses;
            ActivationToken = TokenType.Activation;
        }

        public void GeneratePasswordResetToken()
            => ResetPasswordToken = TokenType.ResetPassword;

        public void SetNewPassword(string newPassword, string resetPasswordToken)
        {
            if (ResetPasswordToken is null)
                throw new SettingNewPasswordException("Missing reset password token");
            if (resetPasswordToken != ResetPasswordToken.Value)
                throw new SettingNewPasswordException("Invalid reset password token");
            Password = new Password(newPassword);
        }

        public void ChangePassword(string oldPassword, string newPassword)
        {
            if (!Password.VerifyPassword(oldPassword))
                throw new ChangingPasswordException("Invalid old password");
            Password = new Password(newPassword);
        }

        public void GenerateRefreshToken()
            => RefreshToken = TokenType.Refresh;

        public void Activate(string activationToken)
        {
            if (ActivationToken is null)
                throw new ActivationOperationException("Missing activation token");
            if (activationToken != ActivationToken.Value)
                throw new ActivationOperationException("Invalid activation token");
            Status = AccountStatus.Active;
            ActivationToken = null;
        }

        public LoginToken Login(string password, ITokenProvider tokenProvider)
        {
            if (!Password.VerifyPassword(password))
                throw new LoginOperationException("Invalid password");
            RefreshToken = TokenType.Refresh;
            return tokenProvider.Provide(RefreshToken.ExpirationDate, RoleOfUser.UserRole, Id);
        }

        public void ChangeRole(UserRole userRole)
        {
            if (RoleOfUser.UserRole != userRole)
                RoleOfUser = userRole;
        }

        public void AddAddress(Address address)
            => Addresses.Add(address);

        public void RemoveAddress(Address address)
            => Addresses.Remove(address);
    }
}
