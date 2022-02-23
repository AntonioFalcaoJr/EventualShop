using Application.EventSourcing.EventStore.Events;
using Infrastructure.DependencyInjection.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Infrastructure.EventSourcing.EventStore.Contexts;

public class EventStoreDbContext : DbContext
{
    private readonly IConfiguration _configuration;
    private readonly SqlServerRetryOptions _options;

    public EventStoreDbContext(DbContextOptions options, IConfiguration configuration, IOptionsSnapshot<SqlServerRetryOptions> optionsSnapshot)
        : base(options)
    {
        _configuration = configuration;
        _options = optionsSnapshot.Value;
    }

    public DbSet<AccountStoreEvent> AccountStoreEvents { get; set; }
    public DbSet<AccountSnapshot> AccountSnapshots { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) 
        => modelBuilder.ApplyConfigurationsFromAssembly(typeof(EventStoreDbContext).Assembly);

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured) return;

        optionsBuilder
            .EnableDetailedErrors()
            .EnableSensitiveDataLogging()
            .UseSqlServer(
                connectionString: _configuration.GetConnectionString("EventStore") ?? string.Empty,
                sqlServerOptionsAction: SqlServerOptionsAction);
    }

    private void SqlServerOptionsAction(SqlServerDbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
            .ExecutionStrategy(
                dependencies => new SqlServerRetryingExecutionStrategy(
                    dependencies: dependencies,
                    maxRetryCount: _options.MaxRetryCount,
                    maxRetryDelay: _options.MaxRetryDelay,
                    errorNumbersToAdd: _options.ErrorNumbersToAdd))
            .MigrationsAssembly(typeof(EventStoreDbContext).Assembly.GetName().Name);

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        => configurationBuilder
            .Properties<string>()
            .AreUnicode(false)
            .HaveMaxLength(1024);
}