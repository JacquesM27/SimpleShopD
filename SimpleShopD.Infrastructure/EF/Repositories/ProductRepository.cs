using Microsoft.EntityFrameworkCore;
using SimpleShopD.Domain.Products;
using SimpleShopD.Domain.Repositories;
using SimpleShopD.Infrastructure.EF.Contexts;

namespace SimpleShopD.Infrastructure.EF.Repositories
{
    internal sealed class ProductRepository : IProductRepository
    {
        private readonly DbSet<Product> _products;
        private readonly WriteDbContext _context;

        public ProductRepository(WriteDbContext context)
        {
            _context = context;
            _products = _context.Products;
        }

        public async Task<Guid> AddAsync(Product product, CancellationToken cancellationToken = default)
        {
            await _products.AddAsync(product, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return product.Id;
        }

        public async Task DeleteAsync(Product product, CancellationToken cancellationToken = default)
        {
            _products.Remove(product);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> DoesExist(Guid id, CancellationToken cancellationToken = default) 
            => await _products.AnyAsync(x => x.Id == id, cancellationToken);

        public async Task<bool> ExistsByTitleAsync(string name, CancellationToken cancellationToken = default)
            => await _products.AnyAsync(x => x.Title == name, cancellationToken);

        public async Task<Product?> GetAsync(Guid id, CancellationToken cancellationToken = default)
            => await _products.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

        public async Task UpdateAsync(Product product)
        {
            _products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task<IList<Product>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default) 
            => await _products.Where(x => ids.Contains(x.Id)).ToListAsync(cancellationToken: cancellationToken);

        public async Task<IList<Product>> GetAllAsync(CancellationToken cancellationToken = default)
            => await _products.ToListAsync(cancellationToken: cancellationToken);
    }
}
