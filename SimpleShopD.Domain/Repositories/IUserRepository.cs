using SimpleShopD.Domain.Users;

namespace SimpleShopD.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User<Guid>> GetAsync(Guid id, CancellationToken cancellationToken = default);
        Task AddAsync(User<Guid> user, CancellationToken cancellationToken = default);
        Task<bool> IsTheEmailUniqueAsync(string email, CancellationToken cancellation = default);
        Task UpdateAsync(User<Guid> user, CancellationToken cancellation = default);
        Task<bool> IsAdminAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
