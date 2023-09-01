using System.ComponentModel.DataAnnotations;

namespace SimpleShopD.Domain.Models.User.ValueObjects
{
    internal readonly record struct Email
    {
        public string EmailAddress { get; }

        internal Email(string emailAddress)
        {
            if (!new EmailAddressAttribute().IsValid(emailAddress))
                throw new ArgumentException($"The provided value {emailAddress} is not an email address");

            EmailAddress = emailAddress;
        }

        public static implicit operator Email(string value) => new(value);

        public static implicit operator string(Email value) => value.EmailAddress;
    }
}
