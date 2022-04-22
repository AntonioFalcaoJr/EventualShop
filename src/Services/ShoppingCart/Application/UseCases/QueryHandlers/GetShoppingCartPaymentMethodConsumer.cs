using Application.Abstractions.Projections;
using ECommerce.Abstractions.Messages.Queries.Responses;
using ECommerce.Contracts.ShoppingCarts;
using MassTransit;

namespace Application.UseCases.QueryHandlers;

public class GetShoppingCartPaymentMethodConsumer :
    IConsumer<Queries.GetShoppingCartPaymentMethod>,
    IConsumer<Queries.GetShoppingCartPaymentMethods>
{
    private readonly IProjectionsRepository<Projections.IPaymentMethodProjection> _projectionsRepository;

    public GetShoppingCartPaymentMethodConsumer(IProjectionsRepository<Projections.IPaymentMethodProjection> projectionsRepository)
    {
        _projectionsRepository = projectionsRepository;
    }

    public async Task Consume(ConsumeContext<Queries.GetShoppingCartPaymentMethod> context)
    {
        var paymentMethod = await _projectionsRepository.FindAsync(
            predicate: paymentMethod => paymentMethod.ShoppingCartId == context.Message.CartId &&
                                        paymentMethod.Id == context.Message.PaymentMethodId,
            cancellationToken: context.CancellationToken);

        await (paymentMethod is null
            ? context.RespondAsync<NotFound>(new())
            : context.RespondAsync(paymentMethod));
    }

    public async Task Consume(ConsumeContext<Queries.GetShoppingCartPaymentMethods> context)
    {
        var paymentMethods = await _projectionsRepository.GetAllAsync(
            context.Message.Limit,
            context.Message.Offset,
            item => item.ShoppingCartId == context.Message.CartId,
            context.CancellationToken);

        await (paymentMethods is null
            ? context.RespondAsync<NotFound>(new())
            : context.RespondAsync(paymentMethods));
    }
}