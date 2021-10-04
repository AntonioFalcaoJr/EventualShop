using System;
using Application.Abstractions.EventSourcing.EventStore.Events;
using Domain.Entities.ShoppingCarts;

namespace Application.EventSourcing.EventStore.Events
{
    public record ShoppingCartSnapshot : Snapshot<ShoppingCart, Guid>;
}