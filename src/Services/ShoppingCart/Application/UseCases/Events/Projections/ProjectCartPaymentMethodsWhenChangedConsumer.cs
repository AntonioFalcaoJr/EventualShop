using Application.Abstractions.Projections;
using ECommerce.Contracts.ShoppingCarts;
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
        var creditCard = new Projection.CreditCardPaymentMethod
        {
            Amount = context.Message.CreditCard.Amount,
            Expiration = context.Message.CreditCard.Expiration,
            Id = context.Message.CreditCard.Id,
            Number = context.Message.CreditCard.Number,
            HolderName = context.Message.CreditCard.HolderName,
            IsDeleted = false,
            SecurityNumber = context.Message.CreditCard.SecurityNumber,
            CartId = context.Message.CartId
        };

        await _repository.InsertAsync(creditCard, context.CancellationToken);
    }

    public async Task Consume(ConsumeContext<DomainEvent.PayPalAdded> context)
    {
        var paypal = new Projection.PayPalPaymentMethod
        {
            Amount = context.Message.PayPal.Amount,
            Id = context.Message.PayPal.Id,
            Password = context.Message.PayPal.Password,
            IsDeleted = false,
            UserName = context.Message.PayPal.UserName,
            CartId = context.Message.CartId,
        };

        await _repository.InsertAsync(paypal, context.CancellationToken);
    }
}