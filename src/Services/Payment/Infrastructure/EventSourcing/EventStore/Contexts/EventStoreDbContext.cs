using Application.EventSourcing.EventStore.Events;
using Infrastructure.DependencyInjection.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Infrastructure.EventSourcing.EventStore.Contexts;

public class EventStoreDbContext : DbContext
{
    private const string SqlLatin1GeneralCp1CsAs = "SQL_Latin1_General_CP1_CS_AS";
    private readonly IConfiguration _configuration;
    private readonly ILoggerFactory _loggerFactory;
    private readonly SqlServerRetryOptions _options;

    public EventStoreDbContext(
        DbContextOptions options,
        ILoggerFactory loggerFactory,
        IConfiguration configuration,
        IOptionsSnapshot<SqlServerRetryOptions> optionsSnapshot)
        : base(options)
    {
        _loggerFactory = loggerFactory;
        _configuration = configuration;
        _options = optionsSnapshot.Value;
    }

    public DbSet<PaymentStoreEvent> PaymentStoreEvents { get; set; }
    public DbSet<PaymentSnapshot> PaymentSnapshots { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation(SqlLatin1GeneralCp1CsAs);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EventStoreDbContext).Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured) return;

        optionsBuilder
            .EnableDetailedErrors()
            .EnableSensitiveDataLogging()
            .UseSqlServer(
                connectionString: _configuration.GetConnectionString("EventStore"),
                sqlServerOptionsAction: SqlServerOptionsAction)
            .UseLoggerFactory(_loggerFactory);
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