using Application.Abstractions.Projections;
using ECommerce.Abstractions;
using ECommerce.Contracts.ShoppingCarts;
using MassTransit;

namespace Application.UseCases.Queries;

public class GetShoppingCartPaymentMethodConsumer :
    IConsumer<Query.GetShoppingCartPaymentMethod>,
    IConsumer<Query.GetShoppingCartPaymentMethods>
{
    private readonly IProjectionRepository<Projection.IPaymentMethod> _projectionRepository;

    public GetShoppingCartPaymentMethodConsumer(IProjectionRepository<Projection.IPaymentMethod> projectionRepository)
    {
        _projectionRepository = projectionRepository;
    }

    public async Task Consume(ConsumeContext<Query.GetShoppingCartPaymentMethod> context)
    {
        var paymentMethod = await _projectionRepository.FindAsync(
            predicate: paymentMethod => paymentMethod.ShoppingCartId == context.Message.CartId &&
                                        paymentMethod.Id == context.Message.PaymentMethodId,
            cancellationToken: context.CancellationToken);

        await (paymentMethod is null
            ? context.RespondAsync<NotFound>(new())
            : context.RespondAsync(paymentMethod));
    }

    public async Task Consume(ConsumeContext<Query.GetShoppingCartPaymentMethods> context)
    {
        var paymentMethods = await _projectionRepository.GetAllAsync(
            context.Message.Limit,
            context.Message.Offset,
            item => item.ShoppingCartId == context.Message.CartId,
            context.CancellationToken);

        await (paymentMethods is null
            ? context.RespondAsync<NotFound>(new())
            : context.RespondAsync(paymentMethods));
    }
}