using Account.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Account.API.Infrastructure;

public class AccountContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public AccountContext(DbContextOptions<AccountContext> options) : base(options)
    {
        // Don't do this in your Production.
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        DataSeed(modelBuilder);
    }

    private void DataSeed(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<User>()
            .HasData(
                new
                {
                    Id = 1,
                    Login = "admin",
                    Password = "123"
                });
    }
}
