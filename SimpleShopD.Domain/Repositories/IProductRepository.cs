using SimpleShopD.Doman.Products;

namespace SimpleShopD.Domain.Repositories
{
    public interface IProductRepository<T> where T : notnull
    {
        Task<Product<T>> GetAsync(T id, CancellationToken cancellationToken = default);
        Task<Product<T>> AddAsync(Product<T> product, CancellationToken cancellationToken = default);
    }
}
