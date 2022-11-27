using Contracts.Services.Account;
using WebAPI.Abstractions;

namespace WebAPI.APIs.Accounts;

public static class AccountApi
{
    public static RouteGroupBuilder MapAccountApi(this RouteGroupBuilder group)
    {
        group.MapGet("/", ([AsParameters] Requests.ListAccounts request)
            => ApplicationApi.QueryAsync(request, (client, cancellationToken) => client.ListAccountsAsync(request, cancellationToken: cancellationToken)));

        group.MapGet("/{accountId:guid}", ([AsParameters] Requests.GetAccount request)
            => ApplicationApi.QueryAsync(request, (client, cancellationToken) => client.GetAccountAsync(request, cancellationToken: cancellationToken)));

        group.MapDelete("/{accountId:guid}", ([AsParameters] Requests.DeleteAccount request)
            => ApplicationApi.SendCommandAsync<Command.DeleteAccount>(request));

        group.MapGet("/{accountId:guid}/profiles/address", ([AsParameters] Requests.ListAddresses request)
            => ApplicationApi.QueryAsync(request, (client, cancellationToken) => client.ListAddressesAsync(request, cancellationToken: cancellationToken)));

        group.MapPut("/{accountId:guid}/profiles/billing-address", ([AsParameters] Requests.AddBillingAddress request)
            => ApplicationApi.SendCommandAsync<Command.AddBillingAddress>(request));

        group.MapPut("/{accountId:guid}/profiles/shipping-address", ([AsParameters] Requests.AddShippingAddress request)
            => ApplicationApi.SendCommandAsync<Command.AddShippingAddress>(request));

        return group.WithMetadata(new TagsAttribute("Accounts"));
    }
}