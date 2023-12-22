using Domain.Abstractions.Aggregates;
using Domain.Abstractions.EventStore;
using Domain.Abstractions.Identities;
using Domain.Aggregates.CatalogItems;
using Domain.Aggregates.Catalogs;
using Domain.Aggregates.Products;
using Infrastructure.EventStore.Contexts.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EventStore.Contexts.Configurations;

public class CatalogStoreEventConfiguration : StoreEventConfiguration<Catalog, CatalogId>;

public class CatalogItemStoreEventConfiguration : StoreEventConfiguration<CatalogItem, CatalogItemId>;

public class ProductStoreEventConfiguration : StoreEventConfiguration<Product, ProductId>;

public abstract class StoreEventConfiguration<TAggregate, TId> : IEntityTypeConfiguration<StoreEvent<TAggregate, TId>>
    where TAggregate : AggregateRoot<TId>
    where TId : GuidIdentifier, new()
{
    public void Configure(EntityTypeBuilder<StoreEvent<TAggregate, TId>> builder)
    {
        builder.ToTable($"{typeof(TAggregate).Name}StoreEvents");

        builder.HasKey(@event => new { @event.Version, @event.AggregateId });

        builder
            .Property(@event => @event.AggregateId)
            .HasConversion<IdentifierConverter<TId>>()
            .IsRequired();

        builder
            .Property(@event => @event.Event)
            .HasConversion<EventConverter>()
            .IsRequired();

        builder
            .Property(@event => @event.EventType)
            .HasMaxLength(50)
            .IsUnicode(false)
            .IsRequired();

        builder
            .Property(@event => @event.Timestamp)
            .IsRequired();

        builder
            .Property(@event => @event.Version)
            .HasConversion<VersionConverter>()
            .IsRequired();
    }
}