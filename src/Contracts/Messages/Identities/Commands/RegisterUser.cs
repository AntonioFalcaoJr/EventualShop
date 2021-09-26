namespace Messages.Identities.Commands
{
    public interface RegisterUser
    {
        string Password { get; }
        string PasswordConfirmation { get; }
        string Login { get; }
    }
}