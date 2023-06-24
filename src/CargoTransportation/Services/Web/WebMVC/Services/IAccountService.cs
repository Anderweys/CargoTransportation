using WebMVC.Models;

namespace WebMVC.Services;

public interface IAccountService
{
    Task<bool> AddAsync(User user);
    Task<User?> GetAsync(User user);
}
