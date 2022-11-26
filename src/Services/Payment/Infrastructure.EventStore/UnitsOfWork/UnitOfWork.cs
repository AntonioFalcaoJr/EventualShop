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
        => CreateExecutionStrategy().ExecuteAsync(ct => ExecuteTransactionAsync(operationAsync, cancellationToken), cancellationToken);

    public Task CommitAsync(CancellationToken cancellationToken)
        => _dbContext.SaveChangesAsync(cancellationToken);

    private async Task ExecuteTransactionAsync(Func<CancellationToken, Task> operationAsync, CancellationToken cancellationToken)
    {
        await using var transaction = await BeginTransactionAsync(cancellationToken);
        await operationAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);
    }

    private Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken)
        => _dbContext.Database.BeginTransactionAsync(cancellationToken);

    private IExecutionStrategy CreateExecutionStrategy()
        => _dbContext.Database.CreateExecutionStrategy();
}