using Contracts.Services.Account;
using WebAPI.Abstractions;

namespace WebAPI.APIs.Accounts;

public static class AccountApi
{
    public static RouteGroupBuilder MapAccountApi(this RouteGroupBuilder group)
    {
        group.MapGet("/", ([AsParameters] Requests.ListAccounts request)
            => ApplicationApi.GetPagedProjectionAsync<Query.ListAccounts, Projection.AccountDetails>(request.Bus, request, request.CancellationToken));

        group.MapGet("/{accountId:guid}", ([AsParameters] Requests.GetAccount request)
            => ApplicationApi.GetProjectionAsync<Query.GetAccount, Projection.AccountDetails>(request.Bus, request, request.CancellationToken));

        group.MapDelete("/{accountId:guid}", ([AsParameters] Requests.DeleteAccount request)
            => ApplicationApi.SendCommandAsync<Command.DeleteAccount>(request));

        group.MapGet("/{accountId:guid}/profiles/address", ([AsParameters] Requests.ListAddresses request)
            => ApplicationApi.GetPagedProjectionAsync<Query.ListAddresses, Projection.AddressListItem>(request.Bus, request, request.CancellationToken));

        group.MapPut("/{accountId:guid}/profiles/billing-address", ([AsParameters] Requests.AddBillingAddress request)
            => ApplicationApi.SendCommandAsync<Command.AddBillingAddress>(request));

        group.MapPut("/{accountId:guid}/profiles/shipping-address", ([AsParameters] Requests.AddShippingAddress request)
            => ApplicationApi.SendCommandAsync<Command.AddShippingAddress>(request));

        return group.WithMetadata(new TagsAttribute("Accounts"));
    }
}