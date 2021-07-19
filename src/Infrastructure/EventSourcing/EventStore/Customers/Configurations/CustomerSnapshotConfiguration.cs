using Domain.Entities.Customers;
using JsonNet.ContractResolvers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace Infrastructure.EventSourcing.EventStore.Customers.Configurations
{
    public class CustomerSnapshotConfiguration : IEntityTypeConfiguration<CustomerSnapshot>
    {
        public void Configure(EntityTypeBuilder<CustomerSnapshot> builder)
        {
            builder.HasKey(storeEvent => storeEvent.Version);

            builder
                .Property(storeEvent => storeEvent.Version)
                .ValueGeneratedNever();

            builder
                .Property(storeEvent => storeEvent.AggregateId)
                .IsRequired();

            builder
                .Property(storeEvent => storeEvent.AggregateName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .IsRequired();

            builder
                .Property(storeEvent => storeEvent.Aggregate)
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