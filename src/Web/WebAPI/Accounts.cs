using Contracts.Abstractions.Paging;
using Contracts.DataTransferObjects;
using Contracts.Services.Account;
using MassTransit;
using Microsoft.AspNetCore.Http.HttpResults;
using WebAPI.Abstractions;

namespace WebAPI;

public static class Accounts
{
    public static GroupRouteBuilder MapAccountApi(this GroupRouteBuilder group)
    {
        group.MapQuery("/", GetAllAsync);
        group.MapCommand(builder => builder.MapPost("/", CreateAsync));
        
        group.MapQuery("/{accountId:guid}", GetAsync);
        group.MapCommand(builder => builder.MapDelete("/{accountId:guid}", DeleteAsync));
        group.MapCommand(builder => builder.MapPut("/{accountId:guid}/profiles/professional-address", DefineProfessionalAddressAsync));
        group.MapCommand(builder => builder.MapPut("/{accountId:guid}/profiles/residence-address", DefineResidenceAddressAsync));
        
        return group;
    }

    private static Task<Results<Ok<IPagedResult<Projection.Account>>, NoContent, NotFound, Problem>> GetAllAsync(IBus bus, int limit, int offset, CancellationToken ct)
        => ApplicationApi.GetPagedProjectionAsync<Query.GetAccounts, Projection.Account>(bus, new(limit, offset), ct);

    private static Task<AcceptedAtRoute> CreateAsync(IBus bus, Request.CreateAccount request, CancellationToken ct)
        => ApplicationApi.SendCommandAsync<Command.CreateAccount>(bus, new(request.Profile, request.Password, request.PasswordConfirmation, request.WishToReceiveNews, request.AcceptedPolicies), ct);

    private static Task<Results<Ok<Projection.Account>, NoContent, NotFound, Problem>> GetAsync(IBus bus, Guid accountId, CancellationToken ct)
        => ApplicationApi.GetProjectionAsync<Query.GetAccount, Projection.Account>(bus, new(accountId), ct);

    private static Task<AcceptedAtRoute> DeleteAsync(IBus bus, Guid accountId, CancellationToken ct)
        => ApplicationApi.SendCommandAsync<Command.DeleteAccount>(bus, new(accountId), ct);

    private static Task<AcceptedAtRoute> DefineProfessionalAddressAsync(IBus bus, Guid accountId, Dto.Address address, CancellationToken ct)
        => ApplicationApi.SendCommandAsync<Command.AddShippingAddress>(bus, new(accountId, address), ct);

    private static Task<AcceptedAtRoute> DefineResidenceAddressAsync(IBus bus, Guid accountId, Dto.Address address, CancellationToken ct)
        => ApplicationApi.SendCommandAsync<Command.AddBillingAddress>(bus, new(accountId, address), ct);
}