using Asp.Versioning.Builder;
using Contracts.Services.Account;
using WebAPI.Abstractions;

namespace WebAPI.APIs.Accounts;

public static class AccountApi
{
    private const string BaseUrl = "/api/v{version:apiVersion}/accounts/";
    
    public static IVersionedEndpointRouteBuilder MapAccountApiV1(this IVersionedEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup(BaseUrl).HasApiVersion(1);

        group.MapGet("/", ([AsParameters] Requests.ListAccounts request)
            => ApplicationApi.QueryAsync(request, (client, cancellationToken) => client.ListAccountsAsync(request, cancellationToken: cancellationToken)));

        group.MapGet("/{accountId:guid}", ([AsParameters] Requests.GetAccount request)
            => ApplicationApi.QueryAsync(request, (client, cancellationToken) => client.GetAccountAsync(request, cancellationToken: cancellationToken)));

        group.MapDelete("/{accountId:guid}", ([AsParameters] Requests.DeleteAccount request)
            => ApplicationApi.SendCommandAsync<Command.DeleteAccount>(request));

        group.MapGet("/{accountId:guid}/profiles/shipping-addresses", ([AsParameters] Requests.ListShippingAddresses request)
            => ApplicationApi.QueryAsync(request, (client, cancellationToken) => client.ListShippingAddressesAsync(request, cancellationToken: cancellationToken)));

        group.MapPost("/{accountId:guid}/profiles/billing-addresses", ([AsParameters] Requests.AddBillingAddress request)
            => ApplicationApi.SendCommandAsync<Command.AddBillingAddress>(request));

        group.MapPost("/{accountId:guid}/profiles/shipping-addresses", ([AsParameters] Requests.AddShippingAddress request)
            => ApplicationApi.SendCommandAsync<Command.AddShippingAddress>(request));

        return builder;
    }

    public static IVersionedEndpointRouteBuilder MapAccountApiV2(this IVersionedEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup(BaseUrl).HasApiVersion(2);

        group.MapGet("/", ([AsParameters] Requests.ListAccounts request)
            => ApplicationApi.QueryAsync(request, (client, cancellationToken) => client.ListAccountsAsync(request, cancellationToken: cancellationToken)));

        return builder;
    }
}