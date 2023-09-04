namespace SimpleShopD.Domain.Models.Products.ValueObjects
{
    internal readonly record struct Description
    {
        public string Value { get; }

        public Description(string value)
        {
            if(string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(value));

            Value = value;
        }

        public static implicit operator Description(string value) => new(value);

        public static implicit operator string(Description description) => description.Value;
    }
}
