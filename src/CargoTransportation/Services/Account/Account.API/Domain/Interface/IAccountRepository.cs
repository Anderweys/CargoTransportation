using Account.API.Domain.Entities;

namespace Account.API.Domain.Interface;

public interface IAccountRepository
{
    Task<bool> AddAsync(User user);
    Task<User?> GetAsync(User user);
}
