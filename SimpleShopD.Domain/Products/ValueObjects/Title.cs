﻿using SimpleShopD.Domain.Products.Exceptions;

namespace SimpleShopD.Doman.Products.ValueObjects
{
    public sealed record Title
    {
        public string Value { get; }

        public Title(string value)
        {
            if(string.IsNullOrWhiteSpace(value))
                throw new EmptyTitleExeption(nameof(value));

            Value = value;
        }

        public static implicit operator Title(string value) => new(value);

        public static implicit operator string(Title title) => title.Value;
    }
}