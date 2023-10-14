using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleShopD.Domain.Orders;
using SimpleShopD.Domain.Products;
using SimpleShopD.Domain.Users;
using SimpleShopD.Infrastructure.EF.Models;

namespace SimpleShopD.Infrastructure.EF.Config
{
    internal class ReadConfiguration : IEntityTypeConfiguration<UserReadModel>,
        IEntityTypeConfiguration<AddressReadModel>,
        IEntityTypeConfiguration<ProductReadModel>,
        IEntityTypeConfiguration<OrderLineReadModel>,
        IEntityTypeConfiguration<OrderReadModel>
    {
        public void Configure(EntityTypeBuilder<UserReadModel> builder)
        {
            builder.ToTable(nameof(User));

            builder.HasMany(u => u.Addresses)
                .WithOne(u => u.User);
        }

        public void Configure(EntityTypeBuilder<AddressReadModel> builder)
        {
            builder.ToTable("Address");

            builder.Navigation(a => a.User)
                .UsePropertyAccessMode(PropertyAccessMode.Property);

            builder.Navigation(a => a.Order)
                .UsePropertyAccessMode(PropertyAccessMode.Property);
        }

        public void Configure(EntityTypeBuilder<ProductReadModel> builder)
        {
            builder.ToTable(nameof(Product));
        }

        public void Configure(EntityTypeBuilder<OrderLineReadModel> builder)
        {
            builder.ToTable(nameof(OrderLine));

            builder.Navigation(ol => ol.Product)
                .UsePropertyAccessMode(PropertyAccessMode.Property);
        }

        public void Configure(EntityTypeBuilder<OrderReadModel> builder)
        {
            builder.ToTable(nameof(Order));

            builder.Navigation(o => o.DeliveryAddress)
                .UsePropertyAccessMode(PropertyAccessMode.Property);

            builder.HasMany(u => u.OrderLines)
                .WithOne(u => u.Order);
        }
    }
}
