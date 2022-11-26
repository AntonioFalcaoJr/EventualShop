namespace Infrastructure.EventStore.UnitsOfWork;

public interface IUnitOfWork
{
    Task ExecuteAsync(Func<CancellationToken, Task> operationAsync, CancellationToken cancellationToken);
    Task CommitAsync(CancellationToken cancellationToken);
}