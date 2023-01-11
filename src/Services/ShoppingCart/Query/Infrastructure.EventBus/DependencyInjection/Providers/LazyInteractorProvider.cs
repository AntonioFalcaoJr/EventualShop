using Application.Abstractions;
using Contracts.Abstractions.Messages;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.EventBus.DependencyInjection.Providers;

public class LazyInteractorProvider : ILazyInteractorProvider
{
    private readonly IServiceProvider _serviceProvider;

    public LazyInteractorProvider(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task InteractAsync<T>(T message, CancellationToken cancellationToken) where T : IEvent 
        => await _serviceProvider
            .GetRequiredService<IInteractor<T>>()
            .InteractAsync(message, cancellationToken);
}