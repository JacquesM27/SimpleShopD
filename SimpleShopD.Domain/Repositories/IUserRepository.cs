using SimpleShopD.Doman.Users;

namespace SimpleShopD.Domain.Repositories
{
    public interface IUserRepository<T> where T : notnull
    {
        Task<User<T>> GetAsync(T id, CancellationToken cancellationToken = default);
        Task<User<T>> AddAsync(User<T> user, CancellationToken cancellationToken = default);
    }
}
