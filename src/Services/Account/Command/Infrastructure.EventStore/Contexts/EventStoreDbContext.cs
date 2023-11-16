using Domain.Abstractions.EventStore;
using Infrastructure.EventStore.Contexts.Converters;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EventStore.Contexts;

public class EventStoreDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<StoreEvent>? Events { get; set; }
    public DbSet<Snapshot>? Snapshots { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(typeof(EventStoreDbContext).Assembly);

    protected override void ConfigureConventions(ModelConfigurationBuilder builder)
    {
        builder
            .Properties<string>()
            .AreUnicode(false)
            .HaveMaxLength(1024);

        builder
            .Properties<Snapshot>()
            .HaveConversion<AggregateConverter>();

        builder
            .Properties<StoreEvent>()
            .HaveConversion<EventConverter>();
    }
}