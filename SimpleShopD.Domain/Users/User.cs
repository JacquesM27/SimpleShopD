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
            ResetPasswordToken = null;
        }

        public void ChangePassword(string oldPassword, string newPassword)
        {
            if (!Password.VerifyPassword(oldPassword))
                throw new ChangingPasswordException("Invalid old password");
            Password = newPassword;
        }

        // TODO: this method should not return any data, but append the refresh token into the cookie through the context accessor
        // TODO: also lack expiration validation
        public AuthToken GenerateRefreshToken()
        {
            RefreshToken = TokenType.Refresh;
            return new AuthToken(RefreshToken.Value, RefreshToken.ExpirationDate, null);
        }

        public void Activate(string activationToken)
        {
            if (ActivationToken is null)
                throw new ActivationOperationException("Missing activation token");
            if (activationToken != ActivationToken.Value)
                throw new ActivationOperationException("Invalid activation token");
            Status = AccountStatus.Active;
            ActivationToken = null;
        }

        // TODO: this method should return only the jwt token and append the refresh token into the cookie through the context accessor
        public AuthToken Login(string password, ITokenProvider tokenProvider)
        {
            if (Status == AccountStatus.Inactive)
                throw new LoginOperationException("Account is not active");
            if (!Password.VerifyPassword(password))
                throw new LoginOperationException("Invalid password");
            RefreshToken = TokenType.Refresh;
            string jwt = tokenProvider.Provide(DateTime.Now.AddMinutes(5), RoleOfUser.UserRole, Id, Email);
            return new AuthToken(RefreshToken.Value, RefreshToken.ExpirationDate, jwt);
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
