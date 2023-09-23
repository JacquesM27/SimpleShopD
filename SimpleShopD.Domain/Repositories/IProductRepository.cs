using SimpleShopD.Domain.Products;

namespace SimpleShopD.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<Product<Guid>> GetAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Guid> AddAsync(Product<Guid> product, CancellationToken cancellationToken = default);
        Task<bool> ExistsByTitleAsync(string name, CancellationToken cancellationToken = default);
        Task DeleteAsync(Product<Guid> product, CancellationToken cancellationToken = default);
        Task<bool> DoesExist(Guid id, CancellationToken cancellationToken = default);
        Task<bool> DoAllExist(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);
        Task<Guid> UpdateAsync(Product<Guid> product);
    }
}
