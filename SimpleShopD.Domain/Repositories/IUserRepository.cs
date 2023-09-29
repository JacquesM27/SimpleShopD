using SimpleShopD.Domain.Users;

namespace SimpleShopD.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetAsync(Guid id, CancellationToken cancellationToken = default);
        Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
        Task AddAsync(User user, CancellationToken cancellationToken = default);
        Task<bool> IsTheEmailUniqueAsync(string email, CancellationToken cancellation = default);
        Task UpdateAsync(User user, CancellationToken cancellation = default);
        Task<bool> IsAdminAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
