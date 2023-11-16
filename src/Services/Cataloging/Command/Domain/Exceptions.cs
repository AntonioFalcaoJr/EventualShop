using Domain.Abstractions;

namespace Domain;

public static class Exceptions
{
    public class InvalidIdentifier() : DomainException<InvalidIdentifier>("Invalid identifier.");

    public class AggregateNotFound() : DomainException<AggregateNotFound>("Aggregate not found.");
}