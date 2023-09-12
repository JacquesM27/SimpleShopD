using SimpleShopD.Domain.Orders;

namespace SimpleShopD.Domain.Repositories
{
    public interface IOrderRepository<T> where T : notnull
    {
        Task<Order<T>> GetAsync(T id, CancellationToken cancellationToken = default);
        Task<Order<T>> AddAsync(Order<T> order, CancellationToken cancellationToken = default);
    }
}
