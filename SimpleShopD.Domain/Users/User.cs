using SimpleShopD.Domain.Addresses;
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
        public IList<Address> Addresses { get; private set; }
        public Token? ActivationToken { get; private set; }
        public Token? RefreshToken { get; private set; }
        public Token? ResetPasswordToken { get; private set; }

        public User(Guid id, Fullname fullname, Email email, Password password,
            RoleOfUser roleOfUser)
            : base(id)
        {
            Fullname = fullname;
            Email = email;
            Password = password;
            Status = AccountStatus.Inactive;
            RoleOfUser = roleOfUser;
            Addresses = new List<Address>();
            ActivationToken = TokenType.Activation;
        }

        private User() : base(Guid.NewGuid()) { }

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
            Password = newPassword;
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

        public void AddAddress(Guid id, string country, string city, string zipCode, string street, string buildingNumber)
            => Addresses.Add(new Address(id, country, city, zipCode, street, buildingNumber));

        public void RemoveAddress(Guid id)
        {
            var address = Addresses.Where(x => x.Id == id).FirstOrDefault();
            if (address is not null)
                Addresses.Remove(address);
        }
    }
}
