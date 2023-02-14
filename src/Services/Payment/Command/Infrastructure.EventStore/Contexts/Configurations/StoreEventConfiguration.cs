using Domain.Abstractions.EventStore;
using Infrastructure.EventStore.Contexts.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EventStore.Contexts.Configurations;

public class StoreEventConfiguration : IEntityTypeConfiguration<StoreEvent>
{
    public void Configure(EntityTypeBuilder<StoreEvent> builder)
    {
        builder.HasKey(storeEvent => new { storeEvent.Version, storeEvent.AggregateId });

        builder
            .Property(storeEvent => storeEvent.Version)
            .IsRequired();

        builder
            .Property(storeEvent => storeEvent.AggregateId)
            .IsRequired();

        builder
            .Property(storeEvent => storeEvent.AggregateType)
            .HasMaxLength(30)
            .IsRequired();

        builder
            .Property(storeEvent => storeEvent.EventType)
            .HasMaxLength(50)
            .IsRequired();

        builder
            .Property(storeEvent => storeEvent.Timestamp)
            .IsRequired();

        builder
            .Property(storeEvent => storeEvent.Event)
            .HasConversion<EventConverter>()
            .IsRequired();
    }
}