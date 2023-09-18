using SimpleShopD.Doman.Users;

namespace SimpleShopD.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User<Guid>> GetAsync(Guid id, CancellationToken cancellationToken = default);
        Task<User<Guid>> AddAsync(User<Guid> user, CancellationToken cancellationToken = default);
    }
}
