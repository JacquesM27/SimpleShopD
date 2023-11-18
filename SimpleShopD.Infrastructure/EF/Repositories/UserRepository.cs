using Microsoft.EntityFrameworkCore;
using SimpleShopD.Domain.Addresses;
using SimpleShopD.Domain.Repositories;
using SimpleShopD.Domain.Users;
using SimpleShopD.Domain.Users.ValueObjects;
using SimpleShopD.Infrastructure.EF.Contexts;

namespace SimpleShopD.Infrastructure.EF.Repositories
{
    internal sealed record UserRepository : IUserRepository
    {
        private readonly DbSet<User> _users;
        private readonly WriteDbContext _context;

        public UserRepository(WriteDbContext context)
        {
            _context = context;
            _users = _context.Users;
        }

        public async Task<Guid> AddAsync(User user, CancellationToken cancellationToken = default)
        {
            await _users.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return user.Id;
        }

        public async Task<bool> DoesExistAsync(Guid id, CancellationToken cancellationToken = default) 
            => await _users.AnyAsync(i => i.Id == id, cancellationToken);

        public async Task<User?> GetAsync(Guid id, CancellationToken cancellationToken = default) 
            => await _users.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

        public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
            => await _users.SingleOrDefaultAsync(x => x.Email == email, cancellationToken);

        public async Task<bool> IsAdminAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var user = await _users.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
            return user != null && Equals(user.UserRole, Role.Admin);
        }

        public async Task<bool> IsTheEmailUniqueAsync(string email, CancellationToken cancellationToken = default)
            => await _users.AnyAsync(i => i.Email == email, cancellationToken);

        public async Task UpdateAsync(User user, CancellationToken cancellationToken = default)
        {
            _users.Update(user);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Address?> GetAddressAsync(Guid userId, Guid addressId, CancellationToken cancellationToken = default) 
            => await _users
                .Where(x => x.Id == userId)
                .SelectMany(c => c.Addresses)
                .Where(x => x.Id == addressId)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        public async Task<User?> GetByRefresh(string refreshToken, CancellationToken cancellationToken = default) 
            => await _users
                .Where(x => x.RefreshToken!.Value == refreshToken)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }
}
