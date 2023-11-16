using Application.Abstractions;
using Contracts.Abstractions.Messages;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.EventBus.DependencyInjection.Providers;

public class LazyInteractorProvider(IServiceProvider serviceProvider) : ILazyInteractorProvider
{
    public async Task InteractAsync<T>(T message, CancellationToken cancellationToken) where T : IEvent 
        => await serviceProvider
            .GetRequiredService<IInteractor<T>>()
            .InteractAsync(message, cancellationToken);
}