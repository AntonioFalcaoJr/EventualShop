using System;
using Domain.Abstractions.Events;

namespace Domain.Entities.Customers
{
    public static class Events
    {
        public record CustomerAgeChanged(int Age) : Event;

        public record CustomerNameChanged(string Name) : Event;
        
        public record CustomerDeleted(Guid Id) : Event;

        public record CustomerRegistered(Guid Id, string Name, int Age) : Event;
    }
}