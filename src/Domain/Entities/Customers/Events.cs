using System;
using Domain.Abstractions.Events;

namespace Domain.Entities.Customers
{
    public static class Events
    {
        public record AgeChanged(int Age) : Event;

        public record NameChanged(string Name) : Event;

        public record Registered(Guid Id, string Name, int Age) : Event;
    }
}