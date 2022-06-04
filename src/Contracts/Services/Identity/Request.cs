namespace Contracts.Services.Identity;

public static class Request
{
    public record ChangePassword(string NewPassword, string NewPasswordConfirmation);
}