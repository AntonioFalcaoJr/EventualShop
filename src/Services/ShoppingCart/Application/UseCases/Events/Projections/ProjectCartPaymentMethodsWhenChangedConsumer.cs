using Application.Abstractions.Projections;
using Contracts.Services.ShoppingCart;
using MassTransit;

namespace Application.UseCases.Events.Projections;

public class ProjectCartPaymentMethodsWhenChangedConsumer :
    IConsumer<DomainEvent.CreditCardAdded>,
    IConsumer<DomainEvent.PayPalAdded>,
    IConsumer<DomainEvent.CartDiscarded>
{
    private readonly IProjectionRepository<Projection.IPaymentMethod> _repository;

    public ProjectCartPaymentMethodsWhenChangedConsumer(IProjectionRepository<Projection.IPaymentMethod> repository)
    {
        _repository = repository;
    }

    public Task Consume(ConsumeContext<DomainEvent.CartDiscarded> context)
        => _repository.DeleteAsync(
            filter: item => item.CartId == context.Message.CartId,
            cancellationToken: context.CancellationToken);

    public async Task Consume(ConsumeContext<DomainEvent.CreditCardAdded> context)
    {
        Projection.CreditCard creditCard = new(
            context.Message.CreditCard.Id,
            context.Message.CartId,
            context.Message.CreditCard.Amount,
            context.Message.CreditCard,
            false);

        await _repository.InsertAsync(creditCard, context.CancellationToken);
    }

    public async Task Consume(ConsumeContext<DomainEvent.PayPalAdded> context)
    {
        Projection.PayPal payPal = new(
            context.Message.PayPal.Id,
            context.Message.CartId,
            context.Message.PayPal.Amount,
            context.Message.PayPal,
            false);

        await _repository.InsertAsync(payPal, context.CancellationToken);
    }
}