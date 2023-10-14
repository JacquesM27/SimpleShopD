using SimpleShopD.Domain.Products;

namespace SimpleShopD.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<Product> GetAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Guid> AddAsync(Product product, CancellationToken cancellationToken = default);
        Task<bool> ExistsByTitleAsync(string name, CancellationToken cancellationToken = default);
        Task DeleteAsync(Product product, CancellationToken cancellationToken = default);
        Task<bool> DoesExist(Guid id, CancellationToken cancellationToken = default);
        Task UpdateAsync(Product product);
    }
}
