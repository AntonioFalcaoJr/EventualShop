using Application.Abstractions;
using Application.Services;
using Contracts.Services.ShoppingCart;
using Domain.Aggregates;

namespace Application.UseCases.Commands;

public class AddPaymentMethodInteractor : IInteractor<Command.AddPaymentMethod>
{
    private readonly IApplicationService _applicationService;

    public AddPaymentMethodInteractor(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    public async Task InteractAsync(Command.AddPaymentMethod command, CancellationToken cancellationToken)
    {
        var shoppingCart = await _applicationService.LoadAggregateAsync<ShoppingCart>(command.CartId, cancellationToken);
        shoppingCart.Handle(command);
        await _applicationService.AppendEventsAsync(shoppingCart, cancellationToken);
    }
}