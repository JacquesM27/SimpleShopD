using SimpleShopD.Domain.Enum;

namespace SimpleShopD.Domain.Products.ValueObjects
{
    public sealed record TypeOfProduct(ProductType ProductType)
    {
        public static implicit operator TypeOfProduct(ProductType productType) => new(productType);

        public static implicit operator ProductType(TypeOfProduct type) => type.ProductType;
    }
}
