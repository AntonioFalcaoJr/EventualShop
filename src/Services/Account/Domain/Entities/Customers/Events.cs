using System;
using Domain.Abstractions.Events;

namespace Domain.Entities.Customers
{
    public static class Events
    {
        public record CustomerAgeChanged(int Age) : DomainEvent;

        public record CustomerNameChanged(string Name) : DomainEvent;
        
        public record CustomerDeleted(Guid Id) : DomainEvent;

        public record CustomerRegistered(Guid Id, string Name, int Age) : DomainEvent;
    }
}