using Application.DependencyInjection;
using Infrastructure.EventStore.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Infrastructure.EventStore.DependencyInjection.Extensions;

public static class HostExtensions
{
    public static async Task MigrateEventStoreAsync(this IHost host, CancellationToken token)
    {
        Log.Information("Migrating EventStore");
        await using var scope = host.Services.CreateAsyncScope();
        await using var dbContext = scope.ServiceProvider.GetRequiredService<EventStoreDbContext>();
        await dbContext.Database.MigrateAsync(token);
        await dbContext.Database.EnsureCreatedAsync(token);
    }

    public static async Task SeedDataAsync(this IHost host, CancellationToken token)
    {
        Log.Information("Seeding Data");
        await using var scope = host.Services.CreateAsyncScope();
        var seeder = scope.ServiceProvider.GetRequiredService<ISeeder>();
        await seeder.SeedDataAsync(token);
    }
}