using Application.EventSourcing.EventStore.Events;
using Domain.Entities.Catalogs;
using JsonNet.ContractResolvers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace Infrastructure.EventSourcing.EventStore.Configurations
{
    public class CatalogSnapshotConfiguration : IEntityTypeConfiguration<CatalogSnapshot>
    {
        public void Configure(EntityTypeBuilder<CatalogSnapshot> builder)
        {
            builder.HasKey(snapshot => new { snapshot.AggregateVersion, snapshot.AggregateId });

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
                    catalog => JsonConvert.SerializeObject(catalog),
                    jsonString => JsonConvert.DeserializeObject<Catalog>(jsonString,
                        new JsonSerializerSettings { ContractResolver = new PrivateSetterContractResolver() }))
                .IsRequired();
        }
    }
}