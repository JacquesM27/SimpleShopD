using SimpleShopD.Domain.Orders.Products;

namespace SimpleShopD.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<Product<Guid>> GetAsync(Guid id, CancellationToken cancellationToken = default);
        Task AddAsync(Product<Guid> product, CancellationToken cancellationToken = default);
        Task<bool> ExistsByTitleAsync(string name, CancellationToken cancellationToken = default);
        Task DeleteByIdAsync(Product<Guid> product, CancellationToken cancellationToken = default);
        Task<bool> AreAllExists(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);
        Task UpdateAsync(Product<Guid> product);
    }
}
