using SimpleShopD.Domain.Orders;

namespace SimpleShopD.Domain.Repositories
{
    public interface IOrderRepository
    {
        Task<Order<Guid>> GetAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Guid> AddAsync(Order<Guid> order, CancellationToken cancellationToken = default);
        Task<Guid> UpdateAsync(Order<Guid> order, CancellationToken cancellationToken = default);
        Task<bool> ProductExistsOnSomeOrder(Guid ProductId, CancellationToken cancellationToken = default);
    }
}
