using Microsoft.EntityFrameworkCore;
using SimpleShopD.Domain.Enum;
using SimpleShopD.Domain.Repositories;
using SimpleShopD.Domain.Users;
using SimpleShopD.Infrastructure.EF.Contexts;

namespace SimpleShopD.Infrastructure.EF.Repositories
{
    internal sealed record UserRepository : IUserRepository
    {
        private readonly DbSet<User> _users;
        private readonly WriteDbContext _context;

        public UserRepository(DbSet<User> users, WriteDbContext context)
        {
            _users = users;
            _context = context;
        }

        public async Task<Guid> AddAsync(User user, CancellationToken cancellationToken = default)
        {
            await _users.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return user.Id;
        }

        public async Task<bool> DoesExist(Guid id, CancellationToken cancellationToken = default) 
            => await _users.AnyAsync(i => i.Id == id, cancellationToken);

        public async Task<User> GetAsync(Guid id, CancellationToken cancellationToken = default) 
            => await _users.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

        public async Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
            => await _users.SingleOrDefaultAsync(x => x.Email == email, cancellationToken);

        public async Task<bool> IsAdminAsync(Guid id, CancellationToken cancellationToken = default) 
            => (await _users.SingleOrDefaultAsync(x => x.Id == id, cancellationToken)).RoleOfUser == UserRole.Admin;

        public async Task<bool> IsTheEmailUniqueAsync(string email, CancellationToken cancellationToken = default)
            => await _users.AnyAsync(i => i.Email == email, cancellationToken);

        public async Task UpdateAsync(User user, CancellationToken cancellationToken = default)
        {
            _users.Update(user);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
