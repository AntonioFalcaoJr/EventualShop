using Application.EventSourcing.EventStore.Events;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EventStore.Contexts;

public class EventStoreDbContext : DbContext
{
    public EventStoreDbContext(DbContextOptions options)
        : base(options) { }

    public DbSet<WarehouseStoreEvent> WarehouseStoreEvents { get; set; }
    public DbSet<WarehouseSnapshot> WarehouseSnapshots { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(typeof(EventStoreDbContext).Assembly);

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        => configurationBuilder
            .Properties<string>()
            .AreUnicode(false)
            .HaveMaxLength(1024);
}