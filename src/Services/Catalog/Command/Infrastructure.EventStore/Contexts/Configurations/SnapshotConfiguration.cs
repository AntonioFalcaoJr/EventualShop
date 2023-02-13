using Domain.Abstractions.EventStore;
using Infrastructure.EventStore.Contexts.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EventStore.Contexts.Configurations;

public class SnapshotConfiguration : IEntityTypeConfiguration<Snapshot>
{
    public void Configure(EntityTypeBuilder<Snapshot> builder)
    {
        builder.HasKey(snapshot => new { snapshot.Version, snapshot.AggregateId });

        builder
            .Property(snapshot => snapshot.Version)
            .IsRequired();

        builder
            .Property(snapshot => snapshot.AggregateId)
            .IsRequired();

        builder
            .Property(snapshot => snapshot.AggregateType)
            .HasMaxLength(30)
            .IsRequired();

        builder
            .Property(snapshot => snapshot.Aggregate)
            .HasConversion<AggregateConverter>()
            .IsRequired();
    }
}