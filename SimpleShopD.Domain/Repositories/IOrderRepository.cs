using SimpleShopD.Domain.Orders;

namespace SimpleShopD.Domain.Repositories
{
    public interface IOrderRepository
    {
        Task<Order<Guid>> GetAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Order<Guid>> AddAsync(Order<Guid> order, CancellationToken cancellationToken = default);
    }
}
