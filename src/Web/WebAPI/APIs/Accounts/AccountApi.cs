using Contracts.Services.Account;
using WebAPI.Abstractions;

namespace WebAPI.APIs.Accounts;

public static class AccountApi
{
    public static void MapAccountApi(this RouteGroupBuilder group)
    {
        group.MapQuery("/", ([AsParameters] Requests.ListAccounts request)
            => ApplicationApi.GetPagedProjectionAsync<Query.ListAccounts, Projection.AccountDetails>(request.Bus, request, request.CancellationToken));

        group.MapCommand(builder => builder.MapPost("/", ([AsParameters] Requests.CreateAccount request)
            => ApplicationApi.SendCommandAsync<Command.CreateAccount>(request.Bus, request, request.CancellationToken)));

        group.MapQuery("/{AccountId:guid}", ([AsParameters] Requests.GetAccount request)
            => ApplicationApi.GetProjectionAsync<Query.GetAccount, Projection.AccountDetails>(request.Bus, request, request.CancellationToken));

        group.MapCommand(builder => builder.MapDelete("/{accountId:guid}", ([AsParameters] Requests.DeleteAccount request)
            => ApplicationApi.SendCommandAsync<Command.DeleteAccount>(request.Bus, request, request.CancellationToken)));

        group.MapQuery("/{AccountId:guid}/profiles/address", ([AsParameters] Requests.ListAddresses request) 
            => ApplicationApi.GetPagedProjectionAsync<Query.ListAddresses, Projection.AddressListItem>(request.Bus, request, request.CancellationToken));

        group.MapCommand(builder => builder.MapPut("/{AccountId:guid}/profiles/billing-address", ([AsParameters] Requests.AddBillingAddress request)
            => ApplicationApi.SendCommandAsync<Command.AddBillingAddress>(request.Bus, request, request.CancellationToken)));

        group.MapCommand(builder => builder.MapPut("/{AccountId:guid}/profiles/shipping-address", ([AsParameters] Requests.AddShippingAddress request) 
            => ApplicationApi.SendCommandAsync<Command.AddShippingAddress>(request.Bus, request, request.CancellationToken)));
        
        group.WithMetadata(new TagsAttribute("Accounts"));
    }
}