using Application.UseCases.Events;
using Contracts.Boundaries.Shopping.ShoppingCart;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers.Events;

public class CartItemAddedConsumer(IReserveInventoryItemWhenCartItemAddedInteractor interactor)
    : Consumer<DomainEvent.CartItemAdded>(interactor);