using Contracts.Services.Order;
using WebAPI.Abstractions;

namespace WebAPI.APIs.Orders;

public static class OrderApi
{
    public static RouteGroupBuilder MapOrderApi(this RouteGroupBuilder group)
    {
        group.MapPut("/{orderId:guid}/cancel", ([AsParameters] Requests.CancelOrder request)
            => ApplicationApi.SendCommandAsync<Command.CancelOrder>(request));

        return group.WithMetadata(new TagsAttribute("Orders"));
    }
}