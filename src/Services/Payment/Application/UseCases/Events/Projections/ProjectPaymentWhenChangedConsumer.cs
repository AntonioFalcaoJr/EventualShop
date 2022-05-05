using Application.Abstractions.Projections;
using Contracts.Services.Payment;
using Domain.Enumerations;
using MassTransit;

namespace Application.UseCases.Events.Projections;

public class ProjectPaymentWhenChangedConsumer :
    IConsumer<DomainEvent.PaymentCanceled>,
    IConsumer<DomainEvent.PaymentRequested>
{
    private readonly IProjectionRepository<Projection.Payment> _repository;

    public ProjectPaymentWhenChangedConsumer(IProjectionRepository<Projection.Payment> repository)
    {
        _repository = repository;
    }

    public async Task Consume(ConsumeContext<DomainEvent.PaymentCanceled> context)
        => await _repository.UpdateFieldAsync(
            id: context.Message.PaymentId,
            field: payment => payment.Status,
            value: PaymentStatus.Canceled.ToString(),
            cancellationToken: context.CancellationToken);

    public async Task Consume(ConsumeContext<DomainEvent.PaymentRequested> context)
    {
        Projection.Payment payment = new(
            context.Message.PaymentId,
            context.Message.OrderId,
            context.Message.Amount,
            context.Message.BillingAddress,
            context.Message.PaymentMethods,
            context.Message.Status,
            false);

        await _repository.InsertAsync(payment, context.CancellationToken);
    }
}