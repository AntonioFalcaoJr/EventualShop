namespace Contracts.Services.Identities;

public static class Request
{
    public record ChangePassword(string NewPassword, string NewPasswordConfirmation);
}