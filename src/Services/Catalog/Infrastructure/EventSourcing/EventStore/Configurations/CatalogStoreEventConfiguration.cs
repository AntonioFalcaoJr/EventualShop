using Application.EventSourcing.EventStore.Events;
using ECommerce.Abstractions.Events;
using JsonNet.ContractResolvers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace Infrastructure.EventSourcing.EventStore.Configurations;

public class CatalogStoreEventConfiguration : IEntityTypeConfiguration<CatalogStoreEvent>
{
    public void Configure(EntityTypeBuilder<CatalogStoreEvent> builder)
    {
        builder.HasKey(storeEvent => storeEvent.Version);

        builder
            .Property(storeEvent => storeEvent.AggregateId)
            .IsRequired();

        builder
            .Property(storeEvent => storeEvent.AggregateName)
            .HasMaxLength(30)
            .IsUnicode(false)
            .IsRequired();

        builder.Property(storeEvent => storeEvent.EventName)
            .HasMaxLength(50)
            .IsUnicode(false)
            .IsRequired();

        builder
            .Property(storeEvent => storeEvent.Event)
            .IsUnicode(false)
            .HasConversion(
                @event => JsonConvert.SerializeObject(@event, typeof(IEvent),
                    new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.Auto
                    }),
                jsonString => JsonConvert.DeserializeObject<IEvent>(jsonString,
                    new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All,
                        ContractResolver = new PrivateSetterContractResolver()
                    }))
            .IsRequired();
    }
}