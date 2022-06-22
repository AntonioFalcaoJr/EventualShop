using Infrastructure.EventStore.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.EventStore.UnitsOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly EventStoreDbContext _dbContext;

    public UnitOfWork(EventStoreDbContext dbContext)
        => _dbContext = dbContext;

    public Task ExecuteAsync(Func<CancellationToken, Task> operationAsync, CancellationToken cancellationToken)
        => CreateExecutionStrategy().ExecuteAsync(ct => ExecuteTransactionAsync(operationAsync, ct), cancellationToken);

    public Task CommitAsync(CancellationToken ct)
        => _dbContext.SaveChangesAsync(ct);

    private async Task ExecuteTransactionAsync(Func<CancellationToken, Task> operationAsync, CancellationToken ct)
    {
        await using var transaction = await BeginTransactionAsync(ct);
        await operationAsync(ct);
        await transaction.CommitAsync(ct);
    }

    private Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken ct)
        => _dbContext.Database.BeginTransactionAsync(ct);

    private IExecutionStrategy CreateExecutionStrategy()
        => _dbContext.Database.CreateExecutionStrategy();
}