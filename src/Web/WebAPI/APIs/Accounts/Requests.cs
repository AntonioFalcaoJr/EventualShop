using Contracts.DataTransferObjects;
using Contracts.Services.Account;
using MassTransit;

namespace WebAPI.APIs.Accounts;

public static class Requests
{
    public record struct AddShippingAddress(IBus Bus, Guid AccountId, Dto.Address Address,
        CancellationToken CancellationToken)
    {
        public static implicit operator Command.AddShippingAddress(AddShippingAddress request)
            => new(request.AccountId, request.Address);
    }

    public record struct AddBillingAddress(IBus Bus, Guid AccountId, Dto.Address Address,
        CancellationToken CancellationToken)
    {
        public static implicit operator Command.AddBillingAddress(AddBillingAddress request)
            => new(request.AccountId, request.Address);
    }

    public record struct ListAddresses(IBus Bus, Guid AccountId, ushort? Limit, ushort? Offset,
        CancellationToken CancellationToken)
    {
        public static implicit operator Query.ListAddresses(ListAddresses request)
            => new(request.AccountId, request.Limit ?? 0, request.Offset ?? 0);
    }

    public record struct DeleteAccount(IBus Bus, Guid AccountId, CancellationToken CancellationToken)
    {
        public static implicit operator Command.DeleteAccount(DeleteAccount request)
            => new(request.AccountId);
    }

    public record struct GetAccount(IBus Bus, Guid AccountId, CancellationToken CancellationToken)
    {
        public static implicit operator Query.GetAccount(GetAccount request)
            => new(request.AccountId);
    }

    public record struct CreateAccount(IBus Bus, Guid AccountId, Dto.Profile Profile, string Password, string PasswordConfirmation, bool AcceptedPolicies, bool WishToReceiveNews, CancellationToken CancellationToken)
    {
        public static implicit operator Command.CreateAccount(CreateAccount request)
            => new(request.AccountId, request.Profile, request.Password, request.PasswordConfirmation, request.AcceptedPolicies, request.WishToReceiveNews);
    }

    public record struct ListAccounts(IBus Bus, ushort? Limit, ushort? Offset, CancellationToken CancellationToken)
    {
        public static implicit operator Query.ListAccounts(ListAccounts request)
            => new(request.Limit ?? 0, request.Offset ?? 0);
    }
}