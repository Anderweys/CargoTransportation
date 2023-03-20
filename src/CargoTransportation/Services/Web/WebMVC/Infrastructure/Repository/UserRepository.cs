using WebMVC.Models;
using MongoDB.Driver;

namespace WebMVC.Infrastructure.Repository;

public class UserRepository : IUserRepository
{
    private readonly IMongoCollection<User> _users;

    public UserRepository(IMongoDatabase mongoDatabase)
    {
        _users = mongoDatabase.GetCollection<User>("Users");

        // Add default user - admin.
        InitData();
    }

    public async Task<bool> AddAsync(User user)
    {
        var isExists = await _users
            .Find(u => u.Login == user.Login)
            .FirstOrDefaultAsync() is not null;

        if (isExists)
        {
            return false;
        }
        _users.InsertOne(user);

        return true;
    }

    public async Task<User> GetAsync(User user)
        => await _users
            .Find(u => u.Login == user.Login && u.Password == user.Password)
            .FirstOrDefaultAsync();

    private void InitData()
    {
        var admin = new User()
        {
            Login = "admin",
            Password ="123"
        };

        _users.InsertOne(admin);
    }
}
