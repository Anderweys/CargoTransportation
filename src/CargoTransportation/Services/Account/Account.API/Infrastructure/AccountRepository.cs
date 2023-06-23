using Account.API.Domain.Entities;
using Account.API.Domain.Interface;
using Microsoft.EntityFrameworkCore;

namespace Account.API.Infrastructure;

public class AccountRepository : IAccountRepository
{
    private readonly AccountContext _context;

    public AccountRepository(AccountContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<bool> AddAsync(User user)
    {
        var isExist = await _context.Users
            .FirstOrDefaultAsync(u => u.Login == user.Login && u.Password == user.Password);
        if (isExist is null)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return true;
        }
        else
        {
            return false;
        }
    }

    public async Task<User?> GetAsync(User user)
         => await _context.Users
            .FirstOrDefaultAsync(u => u.Login == user.Login && u.Password == user.Password);
}
