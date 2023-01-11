using Contracts.Abstractions.Messages;

namespace Infrastructure.EventBus.DependencyInjection.Providers;

public interface ILazyInteractorProvider
{
    Task InteractAsync<T>(T message, CancellationToken cancellationToken) where T : IEvent;
}