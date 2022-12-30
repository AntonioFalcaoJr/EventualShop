using Asp.Versioning.Builder;
using Contracts.Services.Account;
using Contracts.Services.Account.Protobuf;
using WebAPI.Abstractions;

namespace WebAPI.APIs.Accounts;

public static class AccountApi
{
    private const string BaseUrl = "/api/v{version:apiVersion}/accounts/";

    public static IVersionedEndpointRouteBuilder MapAccountApiV1(this IVersionedEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup(BaseUrl).HasApiVersion(1);

        group.MapGet("/accounts-details", ([AsParameters] Requests.ListAccountsDetails request)
            => ApplicationApi.ListAsync<AccountService.AccountServiceClient, AccountDetails>(request, (client, cancellationToken)
                => client.ListAccountsDetailsAsync(request, cancellationToken: cancellationToken)));

        group.MapGet("/{accountId:guid}/account-details", ([AsParameters] Requests.GetAccountDetails request)
            => ApplicationApi.GetAsync<AccountService.AccountServiceClient, AccountDetails>(request, (client, cancellationToken)
                => client.GetAccountDetailsAsync(request, cancellationToken: cancellationToken)));

        group.MapDelete("/{accountId:guid}", ([AsParameters] Requests.DeleteAccount request)
            => ApplicationApi.SendCommandAsync<Command.DeleteAccount>(request));

        group.MapGet("/{accountId:guid}/shipping-addresses/list-items", ([AsParameters] Requests.ListShippingAddressesListItems request)
            => ApplicationApi.ListAsync<AccountService.AccountServiceClient, AddressListItem>(request, (client, cancellationToken)
                => client.ListShippingAddressesListItemsAsync(request, cancellationToken: cancellationToken)));

        // group.MapGet("/{accountId:guid}/billing-addresses/list-items", ([AsParameters] Requests.ListBillingAddressesListItems request)
        //     => ApplicationApi.ListAsync<AccountService.AccountServiceClient, AddressListItem>(request, (client, cancellationToken) 
        //         => client.ListBillingAddressesListItemsAsync(request, cancellationToken: cancellationToken)));

        group.MapPost("/{accountId:guid}/billing-addresses", ([AsParameters] Requests.AddBillingAddress request)
            => ApplicationApi.SendCommandAsync<Command.AddBillingAddress>(request));

        group.MapPost("/{accountId:guid}/shipping-addresses", ([AsParameters] Requests.AddShippingAddress request)
            => ApplicationApi.SendCommandAsync<Command.AddShippingAddress>(request));

        return builder;
    }

    public static IVersionedEndpointRouteBuilder MapAccountApiV2(this IVersionedEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup(BaseUrl).HasApiVersion(2);

        group.MapGet("/accounts-details", ([AsParameters] Requests.ListAccountsDetails request)
            => ApplicationApi.ListAsync<AccountService.AccountServiceClient, AccountDetails>(request, (client, cancellationToken)
                => client.ListAccountsDetailsAsync(request, cancellationToken: cancellationToken)));

        return builder;
    }
}