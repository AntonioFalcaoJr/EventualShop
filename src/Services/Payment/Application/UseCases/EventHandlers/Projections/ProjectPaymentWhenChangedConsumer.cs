using Application.Abstractions.Projections;
using Domain.Enumerations;
using ECommerce.Contracts.Payments;
using MassTransit;

namespace Application.UseCases.EventHandlers.Projections;

public class ProjectPaymentWhenChangedConsumer :
    IConsumer<DomainEvents.PaymentCanceled>,
    IConsumer<DomainEvents.PaymentRequested>
{
    private readonly IProjectionRepository<ECommerce.Contracts.Payments.Projections.Payment> _repository;

    public ProjectPaymentWhenChangedConsumer(IProjectionRepository<ECommerce.Contracts.Payments.Projections.Payment> repository)
    {
        _repository = repository;
    }

    public async Task Consume(ConsumeContext<DomainEvents.PaymentCanceled> context)
        => await _repository.UpdateFieldAsync(
            id: context.Message.PaymentId,
            field: payment => payment.Status,
            value: PaymentStatus.Canceled.ToString(),
            cancellationToken: context.CancellationToken);

    public async Task Consume(ConsumeContext<DomainEvents.PaymentRequested> context)
    {
        var payment = new ECommerce.Contracts.Payments.Projections.Payment
        {
            Amount = context.Message.Amount,
            Id = context.Message.PaymentId,
            Status = context.Message.Status,
            IsDeleted = false,
            OrderId = context.Message.OrderId
        };

        await _repository.InsertAsync(payment, context.CancellationToken);
    }
}