using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EventStore.Contexts;

public class EventStoreDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema(nameof(EventStore));
        builder.ApplyConfigurationsFromAssembly(typeof(EventStoreDbContext).Assembly);
    }
}