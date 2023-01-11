using Application.Abstractions;
using Application.Services;
using Contracts.Services.ShoppingCart;
using Domain.Aggregates;

namespace Application.UseCases.Commands;

public class CreateCartInteractor : IInteractor<Command.CreateCart>
{
    private readonly IApplicationService _applicationService;

    public CreateCartInteractor(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    public async Task InteractAsync(Command.CreateCart command, CancellationToken cancellationToken)
    {
        ShoppingCart shoppingCart = new();
        shoppingCart.Handle(command);
        await _applicationService.AppendEventsAsync(shoppingCart, cancellationToken);
    }
}