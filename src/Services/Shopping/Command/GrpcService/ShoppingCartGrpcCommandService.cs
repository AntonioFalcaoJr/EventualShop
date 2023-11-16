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
        var command = new StartShopping((CustomerId)cmd.CustomerId);
        
        await sender.Send(command, context.CancellationToken);

        //TODO: Finish this
        return default;
    }

    public override async Task<AddItemResponse> AddItem(AddItemCommand cmd, ServerCallContext context)
    {
        var command = new AddCartItem((CartId)cmd.CartId, (ProductId)cmd.ProductId, (Quantity)cmd.Quantity);
        return new() { ItemId = await sender.Send(command, context.CancellationToken) };
    }
}