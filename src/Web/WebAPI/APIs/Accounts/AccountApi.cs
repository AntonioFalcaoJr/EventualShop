using Asp.Versioning.Builder;
using Contracts.Services.Account.Protobuf;
using WebAPI.Abstractions;

namespace WebAPI.APIs.Accounts;

public static class AccountApi
{
    private const string BaseUrl = "/api/v{version:apiVersion}/accounts/";

    public static IVersionedEndpointRouteBuilder MapAccountApiV1(this IVersionedEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup(BaseUrl).HasApiVersion(1);

        group.MapGet("/accounts-details", ([AsParameters] Queries.ListAccountsDetails query)
            => ApplicationApi.ListAsync<AccountService.AccountServiceClient, AccountDetails>(query, (client, ct)
                => client.ListAccountsDetailsAsync(query, cancellationToken: ct)));

        group.MapDelete("/{accountId:guid}", ([AsParameters] Commands.DeleteAccount deleteAccount)
            => ApplicationApi.SendCommandAsync(deleteAccount));

        group.MapGet("/{accountId:guid}/account-details", ([AsParameters] Queries.GetAccountDetails query)
            => ApplicationApi.GetAsync<AccountService.AccountServiceClient, AccountDetails>(query, (client, ct)
                => client.GetAccountDetailsAsync(query, cancellationToken: ct)));

        group.MapPost("/{accountId:guid}/billing-addresses", ([AsParameters] Commands.AddBillingAddress addBillingAddress)
            => ApplicationApi.SendCommandAsync(addBillingAddress));

        group.MapPost("/{accountId:guid}/shipping-addresses", ([AsParameters] Commands.AddShippingAddress addShippingAddress)
            => ApplicationApi.SendCommandAsync(addShippingAddress));

        group.MapGet("/{accountId:guid}/shipping-addresses/list-items", ([AsParameters] Queries.ListShippingAddressesListItems query)
            => ApplicationApi.ListAsync<AccountService.AccountServiceClient, AddressListItem>(query, (client, ct)
                => client.ListShippingAddressesListItemsAsync(query, cancellationToken: ct)));

        return builder;
    }

    public static IVersionedEndpointRouteBuilder MapAccountApiV2(this IVersionedEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup(BaseUrl).HasApiVersion(2);

        group.MapGet("/accounts-details", ([AsParameters] Queries.ListAccountsDetails query)
            => ApplicationApi.ListAsync<AccountService.AccountServiceClient, AccountDetails>(query, (client, ct)
                => client.ListAccountsDetailsAsync(query, cancellationToken: ct)));

        return builder;
    }
}