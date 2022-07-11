using Contracts.DataTransferObjects;
using Contracts.Services.Account;
using MassTransit;
using WebAPI.Abstractions;

namespace WebAPI;

public static class Accounts
{
    public static void MapAccountApi(this GroupRouteBuilder group)
    {
        group.MapQuery("/", (IBus bus, int? limit, int? offset, CancellationToken ct)
            => ApplicationApi.GetPagedProjectionAsync<Query.ListAccounts, Projection.Account>(bus, new(limit ?? 0, offset ?? 0), ct));

        group.MapCommand(builder => builder.MapPost("/", (IBus bus, Command.CreateAccount command, CancellationToken ct)
            => ApplicationApi.SendCommandAsync(bus, command, ct)));

        group.MapQuery("/{accountId:guid}", (IBus bus, Guid accountId, CancellationToken ct)
            => ApplicationApi.GetProjectionAsync<Query.GetAccount, Projection.Account>(bus, new(accountId), ct));

        group.MapCommand(builder => builder.MapDelete("/{accountId:guid}", (IBus bus, Guid accountId, CancellationToken ct)
            => ApplicationApi.SendCommandAsync<Command.DeleteAccount>(bus, new(accountId), ct)));

        group.MapQuery("/{accountId:guid}/profiles/address", (IBus bus, Guid accountId, int? limit, int? offset, CancellationToken ct)
            => ApplicationApi.GetPagedProjectionAsync<Query.ListAddresses, Projection.Address>(bus, new(accountId, limit ?? 0, offset ?? 0), ct));

        group.MapCommand(builder => builder.MapPut("/{accountId:guid}/profiles/billing-address", (IBus bus, Guid accountId, Dto.Address address, CancellationToken ct)
            => ApplicationApi.SendCommandAsync<Command.AddBillingAddress>(bus, new(accountId, address), ct)));

        group.MapCommand(builder => builder.MapPut("/{accountId:guid}/profiles/shipping-address", (IBus bus, Guid accountId, Dto.Address address, CancellationToken ct)
            => ApplicationApi.SendCommandAsync<Command.AddShippingAddress>(bus, new(accountId, address), ct)));

        group.WithMetadata(new TagsAttribute("Accounts"));
    }
}