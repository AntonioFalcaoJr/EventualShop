using Application.EventSourcing.EventStore.Events;
using Infrastructure.EventSourcing.EventStore.Contexts.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EventSourcing.EventStore.Contexts.Configurations;

public class PaymentStoreEventConfiguration : IEntityTypeConfiguration<PaymentStoreEvent>
{
    public void Configure(EntityTypeBuilder<PaymentStoreEvent> builder)
    {
        builder.HasKey(storeEvent => storeEvent.Version);

        builder
            .Property(storeEvent => storeEvent.AggregateId)
            .IsRequired();

        builder
            .Property(storeEvent => storeEvent.AggregateName)
            .HasMaxLength(30)
            .IsRequired();

        builder
            .Property(storeEvent => storeEvent.EventName)
            .HasMaxLength(50)
            .IsRequired();

        builder
            .Property(storeEvent => storeEvent.Event)
            .HasConversion<EventConverter>()
            .IsRequired();
    }
}