using Application.EventSourcing.Customers.EventStore.Events;
using Domain.Entities.Customers;
using JsonNet.ContractResolvers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace Infrastructure.EventSourcing.Customers.EventStore.Configurations
{
    public class CustomerSnapshotConfiguration : IEntityTypeConfiguration<CustomerSnapshot>
    {
        public void Configure(EntityTypeBuilder<CustomerSnapshot> builder)
        {
            builder.HasKey(snapshot => new {snapshot.AggregateVersion, snapshot.AggregateId});

            builder
                .Property(storeEvent => storeEvent.AggregateVersion)
                .IsRequired();

            builder
                .Property(snapshot => snapshot.AggregateId)
                .IsRequired();

            builder
                .Property(snapshot => snapshot.AggregateName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .IsRequired();

            builder
                .Property(snapshot => snapshot.AggregateState)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasConversion(
                    customer => JsonConvert.SerializeObject(customer),
                    jsonString => JsonConvert.DeserializeObject<Customer>(jsonString,
                        new JsonSerializerSettings {ContractResolver = new PrivateSetterContractResolver()}))
                .IsRequired();
        }
    }
}