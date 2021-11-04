using System;
using Messages.Abstractions.Commands;

namespace Messages.Services.Identities;

public static class Commands
{
    public record RegisterUser(string Email, string FirstName, string Password, string PasswordConfirmation) : Command;

    public record ChangeUserPassword(Guid UserId, string NewPassword, string NewPasswordConfirmation) : Command;

    public record DeleteUser(Guid UserId) : Command;
}