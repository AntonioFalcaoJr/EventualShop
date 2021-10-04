using Application.EventSourcing.EventStore.Events;
using Domain.Aggregates;
using JsonNet.ContractResolvers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace Infrastructure.EventSourcing.EventStore.Configurations
{
    public class ShoppingCartSnapshotConfiguration : IEntityTypeConfiguration<ShoppingCartSnapshot>
    {
        public void Configure(EntityTypeBuilder<ShoppingCartSnapshot> builder)
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
                .IsUnicode(false)
                .IsRequired();

            builder
                .Property(snapshot => snapshot.AggregateState)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasConversion(
                    account => JsonConvert.SerializeObject(account),
                    jsonString => JsonConvert.DeserializeObject<Cart>(jsonString,
                        new JsonSerializerSettings { ContractResolver = new PrivateSetterContractResolver() }))
                .IsRequired();
        }
    }
}