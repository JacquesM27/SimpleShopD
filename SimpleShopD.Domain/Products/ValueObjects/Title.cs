using SimpleShopD.Domain.Products.Exceptions;

namespace SimpleShopD.Domain.Products.ValueObjects
{
    public sealed record Title
    {
        public string Value { get; }

        public Title(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new EmptyTitleExeption("Title cannot be empty.");

            Value = value;
        }

        public static implicit operator Title(string value) => new(value);

        public static implicit operator string(Title title) => title.Value;
    }
}
