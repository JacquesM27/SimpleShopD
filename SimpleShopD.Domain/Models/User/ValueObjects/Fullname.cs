namespace SimpleShopD.Domain.Models.User.ValueObjects
{
    internal readonly record struct Fullname
    {
        public string Firstname { get; }
        public string Lastname { get; }

        public Fullname(string firstname, string lastname)
        {
            if (string.IsNullOrWhiteSpace(firstname))
                throw new ArgumentNullException("Firstname cannot be empty");
            if (string.IsNullOrWhiteSpace(lastname))
                throw new ArgumentNullException("Lastname cannot be empty");

            if(firstname.Length is < 2 or > 100)
                throw new ArgumentOutOfRangeException("Firstname length must be longer than 2 and shorter than 100");
            if (lastname.Length is < 2 or > 100)
                throw new ArgumentOutOfRangeException("Lastname length must be longer than 2 and shorter than 100");

            Firstname = firstname;
            Lastname = lastname;
        }
    }
}
