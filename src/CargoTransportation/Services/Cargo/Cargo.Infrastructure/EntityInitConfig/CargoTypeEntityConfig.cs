using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CargoObject.Domain.AggregatesModel.CargoAggregates;

namespace CargoObject.Infrastructure.EntityInitConfig;

public class CargoTypeEntityConfig : IEntityTypeConfiguration<CargoType>
{
    public void Configure(EntityTypeBuilder<CargoType> builder)
    {
        builder.ToTable(nameof(CargoType));
        builder.HasKey(t => t.Id);

        builder.Ignore(t => t.DomainEvents);
    }
}
