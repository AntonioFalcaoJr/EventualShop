using Application.EventSourcing.EventStore.Events;
using Infrastructure.EventSourcing.EventStore.Contexts.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EventSourcing.EventStore.Contexts.Configurations;

public class ShoppingCartSnapshotConfiguration : IEntityTypeConfiguration<ShoppingCartSnapshot>
{
    public void Configure(EntityTypeBuilder<ShoppingCartSnapshot> builder)
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
            .HasConversion<CartConverter>()
            .IsRequired();
    }
}