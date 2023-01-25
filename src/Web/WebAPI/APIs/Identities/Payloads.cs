namespace WebAPI.APIs.Identities;

public static class Payloads
{
    public record SignUp(string FirstName, string LastName, string Email, string Password, string PasswordConfirmation);
}