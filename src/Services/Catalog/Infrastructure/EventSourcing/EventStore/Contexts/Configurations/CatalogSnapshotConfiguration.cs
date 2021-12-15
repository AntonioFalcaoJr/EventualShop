using Application.EventSourcing.EventStore.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EventSourcing.EventStore.Contexts.Configurations;

public class CatalogSnapshotConfiguration : IEntityTypeConfiguration<CatalogSnapshot>
{
    public void Configure(EntityTypeBuilder<CatalogSnapshot> builder)
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
            .IsRequired();
    }
}