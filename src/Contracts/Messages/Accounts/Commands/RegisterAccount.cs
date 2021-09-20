namespace Messages.Accounts.Commands
{
    public interface RegisterAccount
    {
        string Password { get; }
        string PasswordConfirmation { get; }
        string UserName { get; }
    }
}