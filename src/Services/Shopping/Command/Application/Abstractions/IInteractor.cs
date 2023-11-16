namespace Application.Abstractions;

public interface IInteractor<in TCommand>
    where TCommand : class
{
    Task InteractAsync(TCommand cmd, CancellationToken cancellationToken);
}