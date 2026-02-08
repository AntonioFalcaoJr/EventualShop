using Application.UseCases.ShoppingCarts.Commands;
using Contracts.Abstractions.Protobuf;
using Contracts.Shopping.Commands;
using Domain.Aggregates.Products;
using Domain.Aggregates.ShoppingCarts;
using Domain.ValueObjects;
using Grpc.Core;
using MediatR;

namespace GrpcService;

public class ShoppingCartGrpcCommandService(ISender sender) : ShoppingCommandService.ShoppingCommandServiceBase
{
    public override async Task<CommandResponse> StartShopping(StartShoppingCommand cmd, ServerCallContext context)
    {
        StartShopping startShopping = new((CustomerId)cmd.CustomerId);
        var cartId = await sender.Send(startShopping, context.CancellationToken);

        return Response.Created(cartId);
    }

    public override async Task<CommandResponse> AddItem(AddCartItemCommand cmd, ServerCallContext context)
    {
        AddCartItem addCartItem = new(
            (CartId)cmd.CartId,
            (CustomerId) cmd.CustomerId,
            (ProductId)cmd.ProductId,
            (Quantity)cmd.Quantity);

        var itemId = await sender.Send(addCartItem, context.CancellationToken);

        return Response.Created(itemId);
    }
}

public static class Response
{
    public static CommandResponse Accepted() => new() { Accepted = new() };
    public static CommandResponse Created(string id) => new() { Created = new() { Id = new() { Id = id } } };
    public static CommandResponse NoContent() => new() { NoContent = new() };
    public static CommandResponse NotFound() => new() { NotFound = new() };
    public static CommandResponse Ok() => new() { Ok = new() };
}