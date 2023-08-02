using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CargoObject.Domain.ReadModels.CargoAggregates;

namespace CargoObject.Infrastructure.EntityInitConfig;

public class CargoEntityConfig : IEntityTypeConfiguration<Cargo>
{
    public void Configure(EntityTypeBuilder<Cargo> builder)
    {
        builder.ToTable(nameof(Cargo));
        builder.HasKey(t => t.Id);
        //builder.HasOne(t => t.CargoType)
        //    .WithOne(t => t.Cargo)
        //    .HasForeignKey<CargoType>(t => t.CargoId);

        builder
            .Navigation(c => c.CargoItems)
            .UsePropertyAccessMode(PropertyAccessMode.Property);
    }
}
