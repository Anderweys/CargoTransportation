using WebMVC.Models;

namespace WebMVC.Infrastructure.Repository;

public class UserRepository : IUserRepository
{
    // Fake DbContext.
    private List<User> _users;

    public UserRepository()
    {
        _users= new List<User>()
        {
            new("admin", "1234")
        };
    }

    public Task<bool> AddAsync(User user)
    {
        _users.Add(user);
        return Task.FromResult(true);
    }

    public Task<User> GetAsync(User user)
    {
        var result = _users.Find(u => u.Name == user.Name && u.Email == user.Email);
        return Task.FromResult(result);
    }

    public Task<IEnumerable<User>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<User>>(_users);
    }
}
