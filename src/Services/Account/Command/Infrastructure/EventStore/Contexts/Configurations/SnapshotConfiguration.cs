using Domain.Abstractions.EventStore;
using EventStore.Contexts.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventStore.Contexts.Configurations;

public class SnapshotConfiguration : IEntityTypeConfiguration<Snapshot>
{
    public void Configure(EntityTypeBuilder<Snapshot> builder)
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
            .Property(snapshot => snapshot.Aggregate)
            .HasConversion<AggregateConverter>()
            .IsRequired();
    }
}