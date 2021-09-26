using System;

namespace Messages.Identities.Commands
{
    public interface DeleteUser
    {
        Guid UserId { get; }
    }
}