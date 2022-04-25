namespace ECommerce.Contracts.Identities;

public static class Request
{
    public record ChangePassword(string NewPassword, string NewPasswordConfirmation);
}