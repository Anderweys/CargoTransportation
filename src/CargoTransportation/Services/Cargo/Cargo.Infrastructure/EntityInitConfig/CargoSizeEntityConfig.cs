using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CargoObject.Domain.ReadModels.CargoAggregates;

namespace CargoObject.Infrastructure.EntityInitConfig;

public class CargoSizeEntityConfig : IEntityTypeConfiguration<CargoSize>
{
    public void Configure(EntityTypeBuilder<CargoSize> builder)
    {
        builder.ToTable(nameof(CargoSize));

        builder.Property<int>("Id")
            .IsRequired();

        builder.HasKey("Id");
    }
}
