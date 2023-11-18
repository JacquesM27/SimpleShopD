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
        public Status Status { get; private set; }
        public Role UserRole { get; private set; }
        public IList<Address> Addresses { get; private set; }
        public Token? ActivationToken { get; private set; }
        public Token? RefreshToken { get; private set; }
        public Token? ResetPasswordToken { get; private set; }

        public User(Guid id, Fullname fullname, Email email, Password password,
            Role userRole, string activationToken)
            : base(id)
        {
            Fullname = fullname;
            Email = email;
            Password = password;
            Status = AccountStatus.Inactive;
            UserRole = userRole;
            Addresses = new List<Address>();
            ActivationToken = new Token(TokenType.Activation, activationToken);
        }

        private User() : base(Guid.NewGuid()) { }

        public void GeneratePasswordResetToken(ITokenProvider tokenProvider)
            => ResetPasswordToken = new Token(TokenType.ResetPassword, tokenProvider.GenerateRandomToken());

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

        public AuthResponse GenerateRefreshToken(ITokenProvider tokenProvider, IContextAccessor contextAccessor)
        {
            if (Status == AccountStatus.Inactive)
                throw new RefreshTokenOperationException("Account is not active");
            if (RefreshToken is null)
                throw new RefreshTokenOperationException("Missing refresh");
            if (RefreshToken.IsExpired)
                throw new RefreshTokenOperationException("Refresh is expired");

            RefreshToken = new Token(TokenType.Refresh, tokenProvider.GenerateRandomToken());
            string jwt = tokenProvider.Provide(UserRole, Id, Email);
            contextAccessor.AppendRefreshToken(RefreshToken.Value, RefreshToken.ExpirationDate);
            return new AuthResponse(jwt, Fullname.ToString());
        }

        public void Activate(string activationToken)
        {
            if (Status.Value == AccountStatus.Active)
                return;
            if (ActivationToken is null)
                throw new ActivationOperationException("Missing activation token");
            if (activationToken != ActivationToken.Value)
                throw new ActivationOperationException("Invalid activation token");

            Status = AccountStatus.Active;
            ActivationToken = null;
        }

        public AuthResponse Login(string password, ITokenProvider tokenProvider, IContextAccessor cookieTokenAccessor)
        {
            if (!Password.VerifyPassword(password))
                throw new LoginOperationException("Invalid password");
            if (Status == AccountStatus.Inactive)
                throw new LoginOperationException("Account is not active");

            RefreshToken = new Token(TokenType.Refresh,tokenProvider.GenerateRandomToken());
            string jwt = tokenProvider.Provide( UserRole, Id, Email);
            cookieTokenAccessor.AppendRefreshToken(RefreshToken.Value, RefreshToken.ExpirationDate);
            return new AuthResponse(jwt, Fullname.ToString());
        }

        public void LogOut(IContextAccessor cookieTokenAccessor)
        {
            RefreshToken = null;
            cookieTokenAccessor.RemoveRefreshToken();
        }

        public void ChangeRole(Role userRole)
        {
            if (UserRole != userRole)
                UserRole = userRole;
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
