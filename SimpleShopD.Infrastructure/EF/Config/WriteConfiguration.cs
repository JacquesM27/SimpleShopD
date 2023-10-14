using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SimpleShopD.Domain.Enum;
using SimpleShopD.Domain.Orders;
using SimpleShopD.Domain.Orders.ValueObjects;
using SimpleShopD.Domain.Products;
using SimpleShopD.Domain.Products.ValueObjects;
using SimpleShopD.Domain.Shared.ValueObjects;
using SimpleShopD.Domain.Users;
using SimpleShopD.Domain.Users.ValueObjects;

namespace SimpleShopD.Infrastructure.EF.Config
{
    internal sealed class WriteConfiguration : IEntityTypeConfiguration<User>, 
        IEntityTypeConfiguration<Product>, 
        IEntityTypeConfiguration<OrderLine>,
        IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property<Guid>("Id");

            builder.OwnsOne(u => u.Fullname, fullname =>
            {
                fullname.OwnsOne(f => f.Firstname);
                fullname.OwnsOne(f => f.Lastname);
            });

            var emailConverter = new ValueConverter<Email, string>(e => e,e => e);
            builder.Property(typeof(Email), "_email")
                .HasConversion(emailConverter)
                .HasColumnName(nameof(Email));
            builder.HasIndex("_email")
                .IsUnique();

            builder.OwnsOne(u => u.Password, password =>
            {
                password.OwnsOne(p => p.Hash, hash => 
                { 
                    hash.Property("_hash").HasColumnName("Hash").HasColumnType("varbinary(max)"); 
                });
                password.OwnsOne(p => p.Salt, salt =>
                {
                    salt.Property("_salt").HasColumnName("Salt").HasColumnType("varbinary(max)");
                });
            });

            var statusConverter = new ValueConverter<StatusOfAccount, AccountStatus>(s => s, s => s);
            builder.Property(typeof(StatusOfAccount), "_status")
                .HasConversion(statusConverter)
                .HasColumnName(nameof(StatusOfAccount));
            
            var roleConverter = new ValueConverter<RoleOfUser, UserRole>(r => r, r => r);
            builder.Property(typeof(RoleOfUser), "_role")
                .HasConversion(roleConverter)
                .HasColumnName(nameof(RoleOfUser));

            builder.HasMany(typeof(Address), "_addresses");

            builder.OwnsOne(u => u.ActivationToken, token =>
            {
                token.OwnsOne(t => t.Value);
                token.Property(t => t.ExpirationDate);
            });

            builder.OwnsOne(u => u.RefreshToken, token =>
            {
                token.OwnsOne(t => t.Value);
                token.Property(t => t.ExpirationDate);
            });

            builder.OwnsOne(u => u.ResetPasswordToken, token =>
            {
                token.OwnsOne(t => t.Value);
                token.Property(t => t.ExpirationDate);
            });
            builder.ToTable(nameof(User));
        }

        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property<Guid>("Id");

            var titleConverter = new ValueConverter<Title, string>(t => t, t => t);
            builder.Property(typeof(Title), "_title")
                .HasConversion(titleConverter)
                .HasColumnName(nameof(Title));

            var descriptionConverter = new ValueConverter<Description, string>(d => d, d => d);
            builder.Property(typeof(Description), "_description")
                .HasConversion(titleConverter)
                .HasColumnName(nameof(Description));

            var typeConverter = new ValueConverter<TypeOfProduct, ProductType>(t => t, t => t);
            builder.Property(typeof(TypeOfProduct), "_type")
                .HasConversion(typeConverter)
                .HasColumnName(nameof(Type));

            var priceConverter = new ValueConverter<Price, decimal>(p => p, p => p);
            builder.Property(typeof(Price), "_price")
                .HasConversion(priceConverter)
                .HasColumnName(nameof(Price));

            builder.ToTable(nameof(Product));
        }

        public void Configure(EntityTypeBuilder<OrderLine> builder)
        {
            builder.Property<Guid>("Id");

            var salePriceConverter = new ValueConverter<Price, decimal>(p => p, p => p);
            builder.Property(typeof(Price), "_price")
                .HasConversion(salePriceConverter)
                .HasColumnName(nameof(Price));

            var quantityConverter = new ValueConverter<Quantity, decimal>(q => q, q => q);
            builder.Property(typeof(Quantity), "_quantity")
                .HasConversion(quantityConverter)
                .HasColumnName(nameof(Quantity));

            builder.Property<Guid>("ProductId");
            builder.HasOne<Product>()
                .WithMany()
                .HasForeignKey("OrderLine_Product_FK");
            builder.HasIndex("ProductId");

            builder.ToTable(nameof(OrderLine));
        }

        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property<Guid>("Id");

            builder.OwnsOne(d => d.CreationDate);

            builder.OwnsOne(d => d.LastModifiedDate);

            builder.Property<Guid>("UserId");
            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey("Order_User_FK");
            builder.HasIndex("UserId");

            builder.OwnsOne(o => o.DeliveryAddress, address =>
            {
                address.OwnsOne(a => a.Country);
                address.OwnsOne(a => a.City);
                address.OwnsOne(a => a.ZipCode);
                address.OwnsOne(a => a.Street);
                address.OwnsOne(a => a.BuildingNumber);
            });

            builder.OwnsOne(o => o.ReceiverFullname, fullname =>
            {
                fullname.OwnsOne(f => f.Firstname);
                fullname.OwnsOne(f => f.Lastname);
            });

            var statusConverter = new ValueConverter<StatusOfOrder, OrderStatus>(s => s, s => s);
            builder.Property(typeof(StatusOfOrder), "_statusOfOrder")
                .HasConversion(statusConverter)
                .HasColumnName(nameof(StatusOfOrder));
            builder.HasIndex(nameof(StatusOfOrder));

            builder.HasMany(typeof(OrderLine), "_orderLines");

            builder.ToTable(nameof(Order));
        }
    }
}
