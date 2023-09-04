namespace SimpleShopD.Domain.Models.Products.ValueObjects
{
    internal readonly record struct Title
    {
        public string Value { get; }

        public Title(string value)
        {
            if(string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(value));

            Value = value;
        }

        public static implicit operator Title(string value) => new(value);

        public static implicit operator string(Title title) => title.Value;
    }
}
