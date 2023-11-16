using Application.Abstractions;
using Contracts.Abstractions.Messages;
using MassTransit;

namespace Infrastructure.EventBus.Abstractions;

public abstract class Consumer<TMessage>(IInteractor<TMessage> interactor) : IConsumer<TMessage>
    where TMessage : class, IMessage
{
    public Task Consume(ConsumeContext<TMessage> context)
        => interactor.InteractAsync(context.Message, context.CancellationToken);
}