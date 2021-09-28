using Application.EventSourcing.EventStore.Events;
using JsonNet.ContractResolvers;
using Messages.Abstractions.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace Infrastructure.EventSourcing.EventStore.Configurations
{
    public class AccountStoreEventConfiguration : IEntityTypeConfiguration<AccountStoreEvent>
    {
        public void Configure(EntityTypeBuilder<AccountStoreEvent> builder)
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
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasConversion(
                    domainEvent => JsonConvert.SerializeObject(domainEvent, typeof(IEvent),
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
}