using Application.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Infrastructure.EventStore.UnitsOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly DatabaseFacade _database;

    public UnitOfWork(DbContext dbContext)
        => _database = dbContext.Database;

    public Task ExecuteAsync(Func<CancellationToken, Task> operationAsync, CancellationToken cancellationToken)
        => _database.CreateExecutionStrategy().ExecuteAsync(ct => ExecuteTransactionAsync(operationAsync, ct), cancellationToken);

    private async Task ExecuteTransactionAsync(Func<CancellationToken, Task> operationAsync, CancellationToken cancellationToken)
    {
        await using var transaction = await _database.BeginTransactionAsync(cancellationToken);
        await operationAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);
    }
}