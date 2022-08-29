namespace WebAPI.APIs.Accounts;

public static class Payloads
{
    public record struct CreateAccount(string Email, string Password, string PasswordConfirmation, bool AcceptedPolicies, bool WishToReceiveNews);
}