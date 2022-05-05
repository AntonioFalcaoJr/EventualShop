using Application.Abstractions.Projections;
using Contracts.Services.ShoppingCart;
using MassTransit;

namespace Application.UseCases.Events.Projections;

public class ProjectCartPaymentMethodsWhenChangedConsumer :
    IConsumer<DomainEvent.CreditCardAdded>,
    IConsumer<DomainEvent.PayPalAdded>,
    IConsumer<DomainEvent.CartDiscarded>
{
    private readonly IProjectionRepository<Projection.PaymentMethod> _repository;

    public ProjectCartPaymentMethodsWhenChangedConsumer(IProjectionRepository<Projection.PaymentMethod> repository)
    {
        _repository = repository;
    }

    public Task Consume(ConsumeContext<DomainEvent.CartDiscarded> context)
        => _repository.DeleteAsync(
            filter: item => item.CartId == context.Message.CartId,
            cancellationToken: context.CancellationToken);

    public async Task Consume(ConsumeContext<DomainEvent.CreditCardAdded> context)
    {
        Projection.PaymentMethod creditCard = new(
            context.Message.MethodId,
            context.Message.CartId,
            context.Message.Amount,
            context.Message.CreditCard,
            false);

        await _repository.InsertAsync(creditCard, context.CancellationToken);
    }

    public async Task Consume(ConsumeContext<DomainEvent.PayPalAdded> context)
    {
        Projection.PaymentMethod payPal = new(
            context.Message.MethodId,
            context.Message.CartId,
            context.Message.Amount,
            context.Message.PayPal,
            false);

        await _repository.InsertAsync(payPal, context.CancellationToken);
    }
}