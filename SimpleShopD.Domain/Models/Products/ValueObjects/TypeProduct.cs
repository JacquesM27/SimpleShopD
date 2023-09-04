using SimpleShopD.Domain.Enum;

namespace SimpleShopD.Domain.Models.Products.ValueObjects
{
    internal readonly record struct TypeProduct(ProductType ProductType)
    {
        public static implicit operator TypeProduct(ProductType productType) => new(productType);

        public static implicit operator ProductType(TypeProduct type) => type.ProductType;
    }
}
