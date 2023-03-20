using Microsoft.EntityFrameworkCore;
using CargoObject.Domain.AggregatesModel.CargoAggregates;
using CargoObject.Infrastructure.EntityInitConfig;

namespace CargoObject.Infrastructure;

public class CargoContext : DbContext
{
    public DbSet<Cargo> Cargos { get; set; }
    public DbSet<CargoType> CargoTypes { get; set; }

    public CargoContext(DbContextOptions<CargoContext> options)
        : base(options)
    {
        // Don't do this in your Production.
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CargoPropertyEntityConfig());
        modelBuilder.ApplyConfiguration(new CargoSizeEntityConfig());
        modelBuilder.ApplyConfiguration(new CargoTypeEntityConfig());
        modelBuilder.ApplyConfiguration(new CargoItemEntityConfig());
        modelBuilder.ApplyConfiguration(new CargoEntityConfig());

        // Init beginning data: CargoType.
        DataSeed(modelBuilder);
        
    }

    private void DataSeed(ModelBuilder model)
    {
        // Hasdata incorrect works with shadow key, value object, Owns-one/many.
        // Because we cann't create CargoType with 2 value objects: CargoSize & CargoProperty.
        // And need init all dependency value objects, after init CargoType.
        // So first init CargoSize & CargoProperty.
        // After Init CargoType with there created value objects: CargoSize & CargoProperty.

        // Step 1: Init CargoSize.
        model.Entity<CargoSize>().HasData(new[]
        {
            new{Id = 1, Length = 10.0f, Width = 4.0f, Height = 3.0f, Volume = 10.0f * 4.0f * 3.0f},
            new{Id = 2, Length = 15.0f, Width = 5.0f, Height = 4.0f, Volume = 15.0f * 5.0f * 4.0f},
            new{Id = 3, Length = 20.0f, Width = 6.0f, Height = 6.0f, Volume = 20.0f * 6.0f * 6.0f}
        });

        // Step 2: Init CargoProperty.
        model.Entity<CargoProperty>().HasData(new[]
        {
            new{Id = 1, MaxTemperature = 100.0f, MinTemperature = -50.0f, MaxPressure = 10.0f, MinPressure = -5.0f},
            new{Id = 2, MaxTemperature = 120.0f, MinTemperature = -60.0f, MaxPressure = 12.0f, MinPressure = -6.0f},
            new{Id = 3, MaxTemperature = 160.0f, MinTemperature = -70.0f, MaxPressure = 14.0f, MinPressure = -8.0f}
        });

        // Step 3: Init CargoType and insert IDs value objects.
        // (Yes, Value Object cann't contain ID property, but EF Core cann't work with table without ID. Because we was created shadow key).
        model.Entity<CargoType>().HasData(new[]
        {
            new{Id = 1, TypeName = "small", CargoSizeId = 1, CargoPropertyId = 1},
            new{Id = 2, TypeName = "average", CargoSizeId = 2, CargoPropertyId = 2},
            new{Id = 3, TypeName = "large", CargoSizeId = 3, CargoPropertyId = 3}
        });
    }
}
