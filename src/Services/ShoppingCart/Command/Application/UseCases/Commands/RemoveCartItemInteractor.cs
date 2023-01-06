using Application.Abstractions;
using Application.Services;
using Contracts.Services.ShoppingCart;
using Domain.Aggregates;

namespace Application.UseCases.Commands;

public class RemoveCartItemInteractor : IInteractor<Command.RemoveCartItem>
{
    private readonly IApplicationService _applicationService;

    public RemoveCartItemInteractor(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    public async Task InteractAsync(Command.RemoveCartItem command, CancellationToken cancellationToken)
    {
        var shoppingCart = await _applicationService.LoadAggregateAsync<ShoppingCart>(command.CartId, cancellationToken);
        shoppingCart.Handle(command);
        await _applicationService.AppendEventsAsync(shoppingCart, cancellationToken);
    }
}