using Microsoft.EntityFrameworkCore;
using SimpleShopD.Domain.Orders;
using SimpleShopD.Domain.Repositories;
using SimpleShopD.Infrastructure.EF.Contexts;

namespace SimpleShopD.Infrastructure.EF.Repositories
{
    internal sealed class OrderRepository : IOrderRepository
    {
        private readonly DbSet<Order> _orders;
        private readonly WriteDbContext _context;

        public OrderRepository(WriteDbContext context)
        {
            _context = context;
            _orders = _context.Orders;
        }

        public async Task<Guid> AddAsync(Order order, CancellationToken cancellationToken = default)
        {
            await _orders.AddAsync(order, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return order.Id;
        }

        public async Task<Order?> GetAsync(Guid id, CancellationToken cancellationToken = default)
            => await _orders.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

        public async Task<bool> ProductExistsOnSomeOrder(Guid ProductId, CancellationToken cancellationToken = default)
            => await _orders.AnyAsync(x => x.OrderLines.Any(y => y.ProductId == ProductId), cancellationToken);

        public async Task UpdateAsync(Order order, CancellationToken cancellationToken = default)
        {
            _orders.Update(order);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<Order>> GetByUserAsync(Guid userId, CancellationToken cancellationToken = default) 
            => await _orders.Where(x => x.UserId == userId).ToListAsync(cancellationToken: cancellationToken);
    }
}
