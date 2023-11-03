using Microsoft.EntityFrameworkCore;
using SimpleShopD.Domain.Orders;
using SimpleShopD.Domain.Products;
using SimpleShopD.Domain.Users;

namespace SimpleShopD.Infrastructure.EF.Contexts
{
    internal sealed class WriteDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }

        public WriteDbContext(DbContextOptions<WriteDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("shop");
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            //var configuration = new WriteConfiguration();
            //modelBuilder.ApplyConfiguration<Address>(configuration);
            //modelBuilder.ApplyConfiguration<User>(configuration);
            //modelBuilder.ApplyConfiguration<Product>(configuration);
            //modelBuilder.ApplyConfiguration<Order>(configuration);
            //modelBuilder.ApplyConfiguration<OrderLine>(configuration);
        }
    }
}
