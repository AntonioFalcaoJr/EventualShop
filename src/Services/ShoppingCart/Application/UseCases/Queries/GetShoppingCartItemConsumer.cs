using Application.Abstractions.Projections;
using Contracts.Abstractions;
using Contracts.Services.ShoppingCart;
using MassTransit;

namespace Application.UseCases.Queries;

public class GetShoppingCartItemConsumer :
    IConsumer<Query.GetShoppingCartItem>,
    IConsumer<Query.GetShoppingCartItems>
{
    private readonly IProjectionRepository<Projection.ShoppingCartItem> _repository;

    public GetShoppingCartItemConsumer(IProjectionRepository<Projection.ShoppingCartItem> repository)
    {
        _repository = repository;
    }

    public async Task Consume(ConsumeContext<Query.GetShoppingCartItem> context)
    {
        var shoppingCartItem = await _repository.FindAsync(
            item => item.CartId == context.Message.CartId &&
                    item.Id == context.Message.ItemId,
            context.CancellationToken);

        await (shoppingCartItem is null
            ? context.RespondAsync<NotFound>(new())
            : context.RespondAsync(shoppingCartItem));
    }

    public async Task Consume(ConsumeContext<Query.GetShoppingCartItems> context)
    {
        var shoppingCartItems = await _repository.GetAllAsync(
            limit: context.Message.Limit,
            offset: context.Message.Offset,
            predicate: item => item.CartId == context.Message.CartId,
            cancellationToken: context.CancellationToken);

        await context.RespondAsync(shoppingCartItems switch
        {
            {PageInfo.Size: > 0} => shoppingCartItems,
            {PageInfo.Size: <= 0} => new NoContent(),
            _ => new NotFound()
        });
    }
}