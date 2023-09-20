using SimpleShopD.Domain.Orders;
using SimpleShopD.Doman.Shared.ValueObjects;

namespace SimpleShopD.Domain.Repositories
{
    public interface IOrderRepository
    {
        Task<Order<Guid>> GetAsync(Guid id, CancellationToken cancellationToken = default);
        Task AddAsync(Order<Guid> order, CancellationToken cancellationToken = default);
        Task UpdateAsync(Order<Guid> order, CancellationToken cancellationToken = default);
    }
}
