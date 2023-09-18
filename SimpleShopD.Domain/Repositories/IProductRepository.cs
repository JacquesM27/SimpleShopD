using SimpleShopD.Domain.Orders.Products;

namespace SimpleShopD.Domain.Repositories
{
    public interface IProductRepository<T> where T : notnull
    {
        Task<Product<T>> GetAsync(T id, CancellationToken cancellationToken = default);
        Task<Product<T>> AddAsync(Product<T> product, CancellationToken cancellationToken = default);
        Task<bool> ExistsByTitleAsync(string name, CancellationToken cancellationToken = default);
    }
}
