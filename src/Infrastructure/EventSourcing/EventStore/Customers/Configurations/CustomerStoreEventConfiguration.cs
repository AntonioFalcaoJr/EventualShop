using Domain.Abstractions.Events;
using JsonNet.ContractResolvers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace Infrastructure.EventSourcing.EventStore.Customers.Configurations
{
    public class CustomerStoreEventConfiguration : IEntityTypeConfiguration<CustomerStoreEvent>
    {
        public void Configure(EntityTypeBuilder<CustomerStoreEvent> builder)
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
                    @event => JsonConvert.SerializeObject(@event, 
                        new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.All}),
                    jsonString => JsonConvert.DeserializeObject<IEvent>(jsonString, 
                        new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.All, ContractResolver = new PrivateSetterContractResolver()}))
                .IsRequired();
        }
    }
}