using Application.Abstractions;
using Application.Services;
using Contracts.Services.ShoppingCart;
using Domain.Aggregates;

namespace Application.UseCases.Commands;

public class AddBillingAddressInteractor : IInteractor<Command.AddBillingAddress>
{
    private readonly IApplicationService _applicationService;

    public AddBillingAddressInteractor(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    public async Task InteractAsync(Command.AddBillingAddress command, CancellationToken cancellationToken)
    {
        var shoppingCart = await _applicationService.LoadAggregateAsync<ShoppingCart>(command.CartId, cancellationToken);
        shoppingCart.Handle(command);
        await _applicationService.AppendEventsAsync(shoppingCart, cancellationToken);
    }
}