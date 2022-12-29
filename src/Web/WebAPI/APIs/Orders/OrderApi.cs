using Asp.Versioning.Builder;
using Contracts.Services.Order;
using WebAPI.Abstractions;

namespace WebAPI.APIs.Orders;

public static class OrderApi
{
    private const string BaseUrl = "/api/v{version:apiVersion}/orders/";

    public static IVersionedEndpointRouteBuilder MapOrderApiV1(this IVersionedEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup(BaseUrl).HasApiVersion(1);

        group.MapPut("/{orderId:guid}/cancel", ([AsParameters] Requests.CancelOrder request)
            => ApplicationApi.SendCommandAsync<Command.CancelOrder>(request));

        return builder;
    }

    public static IVersionedEndpointRouteBuilder MapOrderApiV2(this IVersionedEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup(BaseUrl).HasApiVersion(2);

        group.MapPut("/{orderId:guid}/cancel", ([AsParameters] Requests.CancelOrder request)
            => ApplicationApi.SendCommandAsync<Command.CancelOrder>(request));

        return builder;
    }
}