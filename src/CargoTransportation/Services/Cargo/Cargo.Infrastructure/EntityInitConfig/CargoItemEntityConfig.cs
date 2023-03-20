using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CargoObject.Domain.AggregatesModel.CargoAggregates;

namespace CargoObject.Infrastructure.EntityInitConfig;

public class CargoItemEntityConfig : IEntityTypeConfiguration<CargoItem>
{
    public void Configure(EntityTypeBuilder<CargoItem> builder)
    {
        builder.ToTable(nameof(CargoItem));
        builder.HasKey(x => x.Id);

        builder.Ignore(t => t.DomainEvents);
    }
}
