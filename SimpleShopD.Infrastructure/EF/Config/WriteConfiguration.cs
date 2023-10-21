using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleShopD.Domain.Addresses;
using SimpleShopD.Domain.Orders;
using SimpleShopD.Domain.Products;
using SimpleShopD.Domain.Users;

namespace SimpleShopD.Infrastructure.EF.Config
{
    internal sealed class WriteConfiguration : IEntityTypeConfiguration<Address>,
        IEntityTypeConfiguration<User>, 
        IEntityTypeConfiguration<Product>, 
        IEntityTypeConfiguration<OrderLine>,
        IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(x => x.Id);
            
            builder.Property(x => x.Country)
                .HasConversion(x => x.Value, x => new(x))
                .HasMaxLength(100)
                .IsRequired(true);

            builder.Property(x => x.City)
                .HasConversion(x => x.Value, x => new(x))
                .HasMaxLength(200)
                .IsRequired(true);

            builder.Property(x => x.ZipCode)
                .HasConversion(x => x.Value, x => new(x))
                .HasMaxLength(100)
                .IsRequired(true);

            builder.Property(x => x.Street)
                .HasConversion(x => x.Value, x => new(x))
                .HasMaxLength(200)
                .IsRequired(true);

            builder.Property(x => x.BuildingNumber)
                .HasConversion(x => x.Value, x => new(x))
                .HasMaxLength(100)
                .IsRequired(true);
        }

        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);  

            builder.OwnsOne(u => u.Fullname, fullname =>
            {
                fullname.Property(f => f.Firstname)
                .HasMaxLength(100)
                .IsRequired(true);

                fullname.Property(f => f.Lastname)
                .HasMaxLength(100)
                .IsRequired(true);
            });

            builder.Property(x => x.Email)
                .HasConversion(x => x.EmailAddress, x => new(x))
                .HasMaxLength(400)
                .IsRequired(true);
            builder.HasIndex(x => x.Email)
                .IsUnique(true);

            builder.OwnsOne(u => u.Password, password =>
            {
                password.Property(x => x.Hash)
                .IsRequired(true);

                password.Property(x => x.Salt)
                .IsRequired(true);
            });

            builder.Property(x => x.Status)
                .HasConversion(x => (int)x.Value, x => new((Domain.Enum.AccountStatus)x))
                .IsRequired(true);

            builder.Property(x => x.RoleOfUser)
                .HasConversion(x => (int)x.UserRole, x => new((Domain.Enum.UserRole)x))
                .IsRequired(true);

            builder.HasMany(x => x.Addresses)
                .WithOne()
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);

            builder.OwnsOne(u => u.ActivationToken, token =>
            {
                token.Property(t => t.Value)
                .HasMaxLength(200);

                token.Property(t => t.ExpirationDate);
            });

            builder.OwnsOne(u => u.RefreshToken, token =>
            {
                token.Property(t => t.Value)
                .HasMaxLength(200);

                token.Property(t => t.ExpirationDate);
            });

            builder.OwnsOne(u => u.ResetPasswordToken, token =>
            {
                token.Property(t => t.Value)
                .HasMaxLength(200);

                token.Property(t => t.ExpirationDate);
            });
        }

        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .HasConversion(x => x.Value, x => new(x))
                .HasMaxLength(100)
                .IsRequired(true);

            builder.Property(x => x.Description)
                .HasConversion(x => x.Value, x => new(x))
                .HasMaxLength(2000)
                .IsRequired(true);

            builder.Property(x => x.TypeOfProduct)
                .HasConversion(x => (int)x.ProductType, x => new((Domain.Enum.ProductType)x));

            builder.Property(x => x.Price)
                .HasConversion(x => x.Value, x => new(x));
        }

        public void Configure(EntityTypeBuilder<OrderLine> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Price)
                .HasConversion(x => x.Value, x => new(x));

            builder.Property(x => x.Quantity)
                .HasConversion(x => x.Value, x => new(x));
            
            builder.HasOne<Product>()
                .WithMany()
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasIndex(x => x.ProductId);
        }

        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(d => d.CreationDate)
                .IsRequired(true);

            builder.Property(d => d.LastModifiedDate)
                .IsRequired(true);

            builder.Property(x => x.UserId);
            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasIndex(x => x.UserId);

            builder.HasOne<Address>()
                .WithMany()
                .HasForeignKey(x => x.AddressId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasIndex(x => x.AddressId);

            builder.OwnsOne(u => u.ReceiverFullname, fullname =>
            {
                fullname.Property(f => f.Firstname)
                .HasMaxLength(100)
                .IsRequired(true);

                fullname.Property(f => f.Lastname)
                .HasMaxLength(100)
                .IsRequired(true);
            });

            builder.Property(x => x.CurrentStatus)
                .HasConversion(x => (int)x.Value, x => new((Domain.Enum.OrderStatus)x))
                .IsRequired(true);
            builder.HasIndex(x => x.CurrentStatus);

            builder.HasMany(x => x.OrderLines)
                .WithOne()
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
