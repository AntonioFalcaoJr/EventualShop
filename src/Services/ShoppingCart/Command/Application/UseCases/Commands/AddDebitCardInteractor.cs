using Application.Abstractions;
using Application.Services;
using Contracts.Services.ShoppingCart;
using Domain.Aggregates;

namespace Application.UseCases.Commands;

public class AddDebitCardInteractor : IInteractor<Command.AddDebitCard>
{
    private readonly IApplicationService _applicationService;

    public AddDebitCardInteractor(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    public async Task InteractAsync(Command.AddDebitCard command, CancellationToken cancellationToken)
    {
        var shoppingCart = await _applicationService.LoadAggregateAsync<ShoppingCart>(command.CartId, cancellationToken);
        shoppingCart.Handle(command);
        await _applicationService.AppendEventsAsync(shoppingCart, cancellationToken);
    }
}