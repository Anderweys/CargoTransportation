namespace WebMVC.Models;

public interface IUserRepository
{
    Task<bool> AddAsync(User user);
    Task<User> GetAsync(User user);
}
