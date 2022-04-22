using Application.Abstractions.Projections;
using ECommerce.Contracts.ShoppingCarts;
using MassTransit;

namespace Application.UseCases.EventHandlers.Projections;

public class ProjectCartPaymentMethodsWhenChangedConsumer :
    IConsumer<DomainEvents.CreditCardAdded>,
    IConsumer<DomainEvents.PayPalAdded>,
    IConsumer<DomainEvents.CartDiscarded>
{
    private readonly IProjectionsRepository<ECommerce.Contracts.ShoppingCarts.Projections.IPaymentMethod> _projectionsRepository;

    public ProjectCartPaymentMethodsWhenChangedConsumer(IProjectionsRepository<ECommerce.Contracts.ShoppingCarts.Projections.IPaymentMethod> projectionsRepository)
    {
        _projectionsRepository = projectionsRepository;
    }

    public Task Consume(ConsumeContext<DomainEvents.CartDiscarded> context)
        => _projectionsRepository.DeleteAsync(
            filter: item => item.ShoppingCartId == context.Message.CartId,
            cancellationToken: context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvents.CreditCardAdded> context)
    {
        var creditCard = new ECommerce.Contracts.ShoppingCarts.Projections.CreditCardPaymentMethod
        {
            Amount = context.Message.CreditCard.Amount,
            Expiration = context.Message.CreditCard.Expiration,
            Id = context.Message.CreditCard.Id,
            Number = context.Message.CreditCard.Number,
            HolderName = context.Message.CreditCard.HolderName,
            IsDeleted = false,
            SecurityNumber = context.Message.CreditCard.SecurityNumber,
            ShoppingCartId = context.Message.CartId
        };

        return _projectionsRepository.InsertAsync(creditCard, context.CancellationToken);
    }

    public Task Consume(ConsumeContext<DomainEvents.PayPalAdded> context)
    {
        var paypal = new ECommerce.Contracts.ShoppingCarts.Projections.PayPalPaymentMethod()
        {
            Amount = context.Message.PayPal.Amount,
            Id = context.Message.PayPal.Id,
            Password = context.Message.PayPal.Password,
            IsDeleted = false,
            UserName = context.Message.PayPal.UserName,
            ShoppingCartId = context.Message.CartId,
        };

        return _projectionsRepository.InsertAsync(paypal, context.CancellationToken);
    }
}