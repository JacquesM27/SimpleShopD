﻿using SimpleShopD.Domain.Products.Exceptions;

namespace SimpleShopD.Domain.Products.ValueObjects
{
    public sealed record Description
    {
        public string Value { get; }

        public Description(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new EmptyDescriptionException("Description cannot be empty.");

            if (value.Length > 2000)
                throw new InvalidDescriptionException("Description length must be shorter than 2000");

            Value = value;
        }

        public static implicit operator Description(string value) => new(value);

        public static implicit operator string(Description description) => description.Value;
    }
}
