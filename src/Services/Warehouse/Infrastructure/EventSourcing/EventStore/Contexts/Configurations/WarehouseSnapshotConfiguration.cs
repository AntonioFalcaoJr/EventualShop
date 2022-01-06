using Application.EventSourcing.EventStore.Events;
using Infrastructure.EventSourcing.EventStore.Contexts.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EventSourcing.EventStore.Contexts.Configurations;

public class WarehouseSnapshotConfiguration : IEntityTypeConfiguration<WarehouseSnapshot>
{
    public void Configure(EntityTypeBuilder<WarehouseSnapshot> builder)
    {
        builder.HasKey(snapshot => new { snapshot.AggregateVersion, snapshot.AggregateId });

        builder
            .Property(snapshot => snapshot.AggregateVersion)
            .IsRequired();

        builder
            .Property(snapshot => snapshot.AggregateId)
            .IsRequired();

        builder
            .Property(snapshot => snapshot.AggregateName)
            .HasMaxLength(30)
            .IsRequired();

        builder
            .Property(snapshot => snapshot.AggregateState)
            .HasConversion<InventoryItemConverter>()
            .IsUnicode(false)
            .HasMaxLength(2048)
            .IsRequired();
    }
}