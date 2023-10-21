using SimpleShopD.Domain.Users.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace SimpleShopD.Domain.Users.ValueObjects
{
    public sealed record Email
    {
        public string EmailAddress { get; }

        public Email(string emailAddress)
        {
            if (!new EmailAddressAttribute().IsValid(emailAddress))
                throw new InvalidEmailException($"The provided value {emailAddress} is not an email address");

            if(emailAddress.Length > 400)
                throw new InvalidEmailException($"Email length must be shorter than 400");

            EmailAddress = emailAddress;
        }

        public static implicit operator Email(string value) => new(value);

        public static implicit operator string(Email value) => value.EmailAddress;
    }
}
