using Application.Abstractions;
using Contracts.Abstractions.Messages;
using MassTransit;

namespace Infrastructure.EventBus.Abstractions;

public abstract class Consumer<TEvent>(IInteractor<TEvent> interactor) : IConsumer<TEvent>
    where TEvent : class, IEvent
{
    public Task Consume(ConsumeContext<TEvent> context)
        => interactor.InteractAsync(context.Message, context.CancellationToken);
}