using Application.Abstractions.UseCases;
using Contracts.Abstractions.Messages;
using MassTransit;

namespace Infrastructure.MessageBus.Abstractions;

public abstract class Consumer<TEvent> : IConsumer<TEvent>
    where TEvent : class, IEvent
{
    private readonly IInteractor<TEvent> _interactor;

    protected Consumer(IInteractor<TEvent> interactor)
    {
        _interactor = interactor;
    }
    
    public Task Consume(ConsumeContext<TEvent> context)
        => _interactor.InteractAsync(context.Message, context.CancellationToken);
}