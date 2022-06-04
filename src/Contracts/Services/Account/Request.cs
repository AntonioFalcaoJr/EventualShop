using Contracts.DataTransferObjects;

namespace Contracts.Services.Account;

public static class Request
{
    public record CreateAccount(Dto.Profile Profile, string Password, string PasswordConfirmation, bool WishToReceiveNews, bool AcceptedPolicies);
}