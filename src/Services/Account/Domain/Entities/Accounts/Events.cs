using System;
using Domain.Abstractions.Events;

namespace Domain.Entities.Accounts
{
    public static class Events
    {
        public record AccountAgeChanged(int Age) : DomainEvent;

        public record AccountNameChanged(string Name) : DomainEvent;
        
        public record AccountDeleted(Guid Id) : DomainEvent;

        public record AccountRegistered(Guid Id, string Name, int Age) : DomainEvent;
    }
}