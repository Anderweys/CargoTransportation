using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CargoObject.Domain.AggregatesModel.CargoAggregates;

namespace CargoObject.Infrastructure.EntityInitConfig;

public class CargoPropertyEntityConfig : IEntityTypeConfiguration<CargoProperty>
{
    public void Configure(EntityTypeBuilder<CargoProperty> builder)
    {
        builder.ToTable(nameof(CargoProperty));

        builder.Property<int>("Id")
            .IsRequired();
    }
}
