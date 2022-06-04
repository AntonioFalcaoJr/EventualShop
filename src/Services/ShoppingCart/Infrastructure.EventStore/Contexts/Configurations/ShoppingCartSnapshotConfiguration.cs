using Domain;
using Infrastructure.EventStore.Contexts.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EventStore.Contexts.Configurations;

public class ShoppingCartSnapshotConfiguration : IEntityTypeConfiguration<StoreEvents.Snapshot>
{
    public void Configure(EntityTypeBuilder<StoreEvents.Snapshot> builder)
    {
        builder.HasKey(snapshot => new {snapshot.AggregateVersion, snapshot.AggregateId});

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
            .HasConversion<CartConverter>()
            .IsRequired();
    }
}