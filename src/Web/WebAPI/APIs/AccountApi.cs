using Contracts.DataTransferObjects;
using Contracts.Services.Account;
using Contracts.Services.ShoppingCart.Validators;
using FluentValidation;
using FluentValidation.Validators;
using MassTransit;
using WebAPI.Abstractions;
using WebAPI.ValidationAttributes;
using WebAPI.Validations;

namespace WebAPI.APIs;

public static class AccountApi
{
    public static void MapAccountApi(this RouteGroupBuilder group)
    {
        group.MapQuery("/", (IBus bus, ushort? limit, ushort? offset, CancellationToken ct)
            => ApplicationApi.GetPagedProjectionAsync<Query.ListAccounts, Projection.AccountDetails>(bus, new(limit ?? 0, offset ?? 0), ct));

        group.MapCommand(builder => builder.MapPost("/", (IBus bus, Command.CreateAccount command, CancellationToken ct)
            => ApplicationApi.SendCommandAsync(bus, command, ct)));

        group.MapQuery("/{accountId:guid}", (IBus bus, Guid accountId, CancellationToken ct)
            => ApplicationApi.GetProjectionAsync<Query.GetAccount, Projection.AccountDetails>(bus, new(accountId), ct));

        group.MapCommand(builder => builder.MapDelete("/{accountId:guid}", (IBus bus, [NotEmpty] Guid accountId, CancellationToken ct)
            => ApplicationApi.SendCommandAsync<Command.DeleteAccount>(bus, new(accountId), ct)));

        group.MapQuery("/{accountId:guid}/profiles/address", (IBus bus, [NotEmpty] Guid accountId, ushort? limit, ushort? offset, CancellationToken ct)
            => ApplicationApi.GetPagedProjectionAsync<Query.ListAddresses, Projection.AddressListItem>(bus, new(accountId, limit ?? 0, offset ?? 0), ct));

        group.MapCommand(builder => builder.MapPut("/{accountId:guid}/profiles/billing-address", (IBus bus, [NotEmpty] Guid accountId, Dto.Address address, CancellationToken ct)
            => ApplicationApi.SendCommandAsync<Command.AddBillingAddress>(bus, new(accountId, address), ct)));

        group.MapCommand(builder => builder.MapPut("/{AccountId:guid}/profiles/shipping-address", ([AsParameters] AddShippingAddressRequest request)
            => ApplicationApi.SendCommandAsync<Command.AddShippingAddress>(request.Bus, request, request.CancellationToken)));

        group.WithMetadata(new TagsAttribute("Accounts"));
    }
}

public record struct AddShippingAddressRequest(IBus Bus, Guid AccountId, Dto.Address Address,
    CancellationToken CancellationToken)
{
    public static implicit operator Command.AddShippingAddress(AddShippingAddressRequest request)
        => new(request.AccountId, request.Address);
}

public class AddShippingAddressRequestValidator : AbstractValidator<AddShippingAddressRequest>
{
    public AddShippingAddressRequestValidator()
    {
        RuleFor(x => x.AccountId)
            .NotNull()
            .NotEmpty()
            .NotEqual(Guid.Empty);

        RuleFor(x => x.Address)
            .SetValidator(new AddressValidator());
    }
}

public class AddressValidator : AbstractValidator<Dto.Address>
{
    public AddressValidator()
    {
        RuleFor(address => address.City)
            .NotNull()
            .NotEmpty();

        RuleFor(address => address.Country)
            .NotNull()
            .NotEmpty();

        RuleFor(address => address.State)
            .NotNull()
            .NotEmpty();

        RuleFor(address => address.Street)
            .NotNull()
            .NotEmpty();

        RuleFor(address => address.ZipCode)
            .NotNull()
            .NotEmpty();
    }
}