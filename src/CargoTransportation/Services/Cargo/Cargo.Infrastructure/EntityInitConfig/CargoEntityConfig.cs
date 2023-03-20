using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CargoObject.Domain.AggregatesModel.CargoAggregates;

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

        builder.Ignore(t => t.DomainEvents);

        var navigation = builder.Metadata.FindNavigation(nameof(Cargo.CargoItems));
        navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
