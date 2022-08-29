using Contracts.Services.Account;
using WebAPI.Abstractions;

namespace WebAPI.APIs.Accounts;

public static class AccountApi
{
    public static void MapAccountApi(this RouteGroupBuilder group)
    {
        group.MapGet("/", ([AsParameters] Requests.ListAccounts request)
            => ApplicationApi.GetPagedProjectionAsync<Query.ListAccounts, Projection.AccountDetails>(request.Bus, request, request.CancellationToken));

        group.MapPost("/", ([AsParameters] Requests.CreateAccount request)
            => ApplicationApi.SendCommandAsync<Command.CreateAccount>(request));

        group.MapGet("/{AccountId:guid}", ([AsParameters] Requests.GetAccount request)
            => ApplicationApi.GetProjectionAsync<Query.GetAccount, Projection.AccountDetails>(request.Bus, request, request.CancellationToken));

        group.MapDelete("/{AccountId:guid}", ([AsParameters] Requests.DeleteAccount request)
            => ApplicationApi.SendCommandAsync<Command.DeleteAccount>(request));

        group.MapGet("/{AccountId:guid}/profiles/address", ([AsParameters] Requests.ListAddresses request)
            => ApplicationApi.GetPagedProjectionAsync<Query.ListAddresses, Projection.AddressListItem>(request.Bus, request, request.CancellationToken));

        group.MapPut("/{AccountId:guid}/profiles/billing-address", ([AsParameters] Requests.AddBillingAddress request)
            => ApplicationApi.SendCommandAsync<Command.AddBillingAddress>(request));

        group.MapPut("/{AccountId:guid}/profiles/shipping-address", ([AsParameters] Requests.AddShippingAddress request)
            => ApplicationApi.SendCommandAsync<Command.AddShippingAddress>(request));

       group.WithMetadata(new TagsAttribute("Accounts"));
    }
}