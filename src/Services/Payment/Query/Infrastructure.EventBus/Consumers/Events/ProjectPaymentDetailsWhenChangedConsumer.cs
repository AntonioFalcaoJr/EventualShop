using Application.UseCases.Events;
using Contracts.Services.Payment;
using MassTransit;

namespace Infrastructure.EventBus.Consumers.Events;

public class ProjectPaymentDetailsWhenChangedConsumer : 
    IConsumer<DomainEvent.PaymentRequested>,
    IConsumer<DomainEvent.PaymentCanceled>
{
    private readonly IProjectPaymentWhenChangedInteractor _interactor;

    public ProjectPaymentDetailsWhenChangedConsumer(IProjectPaymentWhenChangedInteractor interactor)
    {
        _interactor = interactor;
    }

    public Task Consume(ConsumeContext<DomainEvent.PaymentRequested> context)
        => _interactor.InteractAsync(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.PaymentCanceled> context)
        => _interactor.InteractAsync(context.Message, context.CancellationToken);
}