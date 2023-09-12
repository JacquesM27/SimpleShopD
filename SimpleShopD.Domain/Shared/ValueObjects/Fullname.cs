using SimpleShopD.Domain.Shared.Exceptions;

namespace SimpleShopD.Doman.Shared.ValueObjects
{
    public sealed record Fullname
    {
        public string Firstname { get; }
        public string Lastname { get; }

        public Fullname(string firstname, string lastname)
        {
            if (string.IsNullOrWhiteSpace(firstname))
                throw new EmptyFullnameException("Firstname cannot be empty");
            if (string.IsNullOrWhiteSpace(lastname))
                throw new EmptyFullnameException("Lastname cannot be empty");

            if (firstname.Length is < 2 or > 100)
                throw new InvalidFullnameLengthException("Firstname length must be longer than 2 and shorter than 100");
            if (lastname.Length is < 2 or > 100)
                throw new InvalidFullnameLengthException("Lastname length must be longer than 2 and shorter than 100");

            Firstname = firstname;
            Lastname = lastname;
        }
    }
}
