using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CargoObject.Domain.ReadModels.CargoAggregates;

namespace CargoObject.Infrastructure.EntityInitConfig;

public class CargoTypeEntityConfig : IEntityTypeConfiguration<CargoType>
{
    public void Configure(EntityTypeBuilder<CargoType> builder)
    {
        builder.ToTable(nameof(CargoType));
        builder.Property("Id").IsRequired();

        builder.HasKey(t => t.Id);
    }
}
